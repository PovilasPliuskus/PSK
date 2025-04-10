using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class WorkspaceRepository : IWorkspaceRepository
{
    private readonly TaskManagerContext _context;

    public WorkspaceRepository(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(WorkspaceEntity workspaceEntity)
    {
        await _context.Workspaces.AddAsync(workspaceEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<WorkspaceEntity> GetAsync(Guid id)
    {
        return await _context.Workspaces
            .Include(w => w.CreatedByUserId)
            .FirstAsync(w => w.Id == id);
    }

    public async Task<List<WorkspaceEntity>> GetAllAsync()
    {
        return await _context.Workspaces
            .Include(w => w.CreatedByUserId)
            .ToListAsync();
    }

    public async Task UpdateAsync(WorkspaceEntity workspaceEntity)
    {
        _context.Workspaces.Update(workspaceEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        WorkspaceEntity workspaceEntity = await _context.Workspaces.FirstAsync(w => w.Id == id);
        _context.Workspaces.Remove(workspaceEntity);
        await _context.SaveChangesAsync();
    }
}
