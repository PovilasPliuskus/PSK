using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class SubTaskRepository : ISubTaskRepository
{
    private readonly TaskManagerContext _context;

    public SubTaskRepository(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(SubTaskEntity subTaskEntity)
    {
        await _context.SubTasks.AddAsync(subTaskEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<SubTaskEntity> GetAsync(Guid id)
    {
        return await _context.SubTasks
            .Include(st => st.Task)
                .ThenInclude(t => t.CreatedByUserId)
            .Include(st => st.Task)
                .ThenInclude(t => t.Workspace)
                    .ThenInclude(w => w.CreatedByUserId)
            .Include(st => st.Task)
                .ThenInclude(t => t.AssignedToUserId)
            .Include(st => st.CreatedByUserId)
            .Include(st => st.AssignedToUserId)
            .FirstAsync(st => st.Id == id);
    }

    public async Task<List<SubTaskEntity>> GetAllAsync()
    {
        return await _context.SubTasks
            .Include(st => st.Task)
                .ThenInclude(t => t.CreatedByUserId)
            .Include(st => st.Task)
                .ThenInclude(t => t.Workspace)
                    .ThenInclude(w => w.CreatedByUserId)
            .Include(st => st.Task)
                .ThenInclude(t => t.AssignedToUserId)
            .Include(st => st.CreatedByUserId)
            .Include(st => st.AssignedToUserId)
            .ToListAsync();
    }

    public async Task UpdateAsync(SubTaskEntity subTaskEntity)
    {
        _context.SubTasks.Update(subTaskEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        SubTaskEntity subTaskEntity = await _context.SubTasks.FirstAsync(st => st.Id == id);
        _context.SubTasks.Remove(subTaskEntity);
        await _context.SaveChangesAsync();
    }
}
