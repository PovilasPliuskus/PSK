using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagerContext _context;

    public TaskRepository(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(TaskEntity taskEntity)
    {
        await _context.Tasks.AddAsync(taskEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<TaskEntity> GetAsync(Guid id)
    {
        return await _context.Tasks
            .Include(t => t.CreatedByUserId)
            .Include(t => t.Workspace)
                .ThenInclude(w => w.CreatedByUserId)
            .Include(t => t.AssignedToUserId)
            .FirstAsync(t => t.Id == id);
    }

    public async Task<List<TaskEntity>> GetAllAsync()
    {
        return await _context.Tasks
            .Include(t => t.CreatedByUserId)
            .Include(t => t.Workspace)
                .ThenInclude(w => w.CreatedByUserId)
            .Include(t => t.AssignedToUserId)
            .ToListAsync();
    }
    public async Task UpdateAsync(TaskEntity taskEntity)
    {
        _context.Tasks.Update(taskEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        TaskEntity taskEntity = await _context.Tasks.FirstAsync(t => t.Id == id);
        _context.Tasks.Remove(taskEntity);
        await _context.SaveChangesAsync();
    }
}
