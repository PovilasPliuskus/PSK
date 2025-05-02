using AutoMapper;
using Contracts.Models;
using Contracts.Pagination;
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

    public async Task<PaginatedResult<WorkspaceWithoutTasks>> GetRangeAsync(int pageNumber, int pageSize)
    {
        List<WorkspaceEntity> workspaceEntities = await _context.Workspaces
            .Paginate(pageNumber, pageSize)
            .ToListAsync();

        int workspaceCount = await _context.Workspaces.CountAsync();

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

    public async Task AddAsync(WorkspaceWithoutTasks workspace)
    {
        WorkspaceEntity workspaceEntity = _mapper.Map<WorkspaceEntity>(workspace);

        await _context.Workspaces.AddAsync(workspaceEntity);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WorkspaceWithoutTasks workspace)
    {
        WorkspaceEntity existingEntity = await _context.Workspaces
            .FirstAsync(w => w.Id == workspace.Id);

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