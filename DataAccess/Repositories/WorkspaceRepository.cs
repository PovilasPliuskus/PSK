using AutoMapper;
using Contracts.Models;
using Contracts.Pagination;
using Contracts.RequestBodies;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Extensions;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Repositories;

public class WorkspaceRepository : IWorkspaceRepository
{
    private readonly TaskManagerContext _context;
    private readonly IMapper _mapper;

    public WorkspaceRepository(TaskManagerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<WorkspaceWithoutTasks>> GetRangeAsync(int pageNumber, int pageSize, string userEmail)
    {
        var query = _context.Workspaces
        .Where(w => _context.WorkspaceUsers
            .Any(wu => wu.FkWorkspaceId == w.Id && wu.FkUserEmail == userEmail));

        List<WorkspaceEntity> workspaceEntities = await query
            .Paginate(pageNumber, pageSize)
            .ToListAsync();

        int workspaceCount = await query.CountAsync();

        var workspaces = _mapper.Map<List<WorkspaceWithoutTasks>>(workspaceEntities);

        return new PaginatedResult<WorkspaceWithoutTasks>(workspaces, workspaceCount, pageNumber, pageSize);
    }

    public async Task<Workspace> GetSingleAsync(Guid id)
    {
        WorkspaceEntity workspaceEntity = await _context.Workspaces
            .Include(w => w.Tasks)
            .FirstAsync(w => w.Id == id);

        return _mapper.Map<Workspace>(workspaceEntity);
    }

    public async Task<PaginatedResult<WorkspaceUser>> GetUsersInWorkspaceAsync(int pageNumber, int pageSize, Guid workspaceId, string userEmail)
    {
        var query = _context.WorkspaceUsers
            .Where(wu => wu.FkWorkspaceId == workspaceId && wu.FkUserEmail != userEmail);

        List<WorkspaceUsersEntity> workspaceUsersEntities = await query
            .Paginate(pageNumber, pageSize)
            .ToListAsync();

        int usersCount = await query.CountAsync();

        var users = _mapper.Map<List<WorkspaceUser>>(workspaceUsersEntities);

        return new PaginatedResult<WorkspaceUser>(users, usersCount, pageNumber, pageSize);
    }

    public async Task AddUserToWorkspaceAsync(Guid workspaceId, AddUserRequest request)
    {
        var workspaceUsersEntity = new WorkspaceUsersEntity
        {
            FkUserEmail = request.UserEmail,
            FkWorkspaceId = workspaceId,
            IsOwner = false
        };

        await _context.WorkspaceUsers.AddAsync(workspaceUsersEntity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveUserFromWorkspaceAsync(Guid workspaceId, RemoveUserRequest request)
    {
        var workspaceUsersEntity = await _context.WorkspaceUsers
            .FirstAsync(wu => wu.FkWorkspaceId == workspaceId && wu.FkUserEmail == request.UserEmail);

        _context.WorkspaceUsers.Remove(workspaceUsersEntity);
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(WorkspaceWithoutTasks workspace)
    {
        WorkspaceEntity workspaceEntity = _mapper.Map<WorkspaceEntity>(workspace);

        // Create a new workspace entity with workspace user as owner
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            await _context.Workspaces.AddAsync(workspaceEntity);

            var workspaceUsersEntity = new WorkspaceUsersEntity
            {
                FkUserEmail = workspace.CreatedByUserEmail,
                FkWorkspaceId = workspaceEntity.Id,
                IsOwner = true
            };

            await _context.WorkspaceUsers.AddAsync(workspaceUsersEntity);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }

    public async Task UpdateAsync(WorkspaceWithoutTasks workspace)
    {
        WorkspaceEntity existingEntity = await _context.Workspaces
            .FirstAsync(w => w.Id == workspace.Id);

        if (!workspace.Force && existingEntity.Version != workspace.Version)
        {
            throw new DbUpdateConcurrencyException("The workspace has been modified by another user.");
        }

        existingEntity.Name = workspace.Name;
        existingEntity.FkCreatedByUserEmail = workspace.CreatedByUserEmail;

        _context.Workspaces.Update(existingEntity);

        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        WorkspaceEntity workspaceEntity = await _context.Workspaces
            .Include(w => w.Tasks)
            .FirstAsync(w => w.Id == id);

        _context.Workspaces.Remove(workspaceEntity);

        await _context.SaveChangesAsync();
    }
}