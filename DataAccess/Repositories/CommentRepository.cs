using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class CommentRepository :  ICommentRepository
{
    private readonly TaskManagerContext _context;

    public CommentRepository(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CommentEntity commentEntity)
    {
        await _context.Comments.AddAsync(commentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<CommentEntity> GetAsync(Guid id)
    {
        return await _context.Comments
            .Include(c => c.Task)
                .ThenInclude(t => t.CreatedByUserId)
            .Include(c => c.Task)
                .ThenInclude(t => t.Workspace)
                    .ThenInclude(w => w.CreatedByUserId)
            .Include(c => c.Task)
                .ThenInclude(t => t.AssignedToUserId)
            .Include(c => c.SubTask)
                .ThenInclude(st => st.Task)
                    .ThenInclude(t => t.CreatedByUserId)
            .Include(c => c.SubTask)
                .ThenInclude(st => st.Task)
                    .ThenInclude(t => t.Workspace)
                        .ThenInclude(w => w.CreatedByUserId)
            .Include(c => c.SubTask)
                .ThenInclude(st => st.Task)
                    .ThenInclude(t => t.AssignedToUserId)
            .Include(c => c.SubTask)
                .ThenInclude(st => st.CreatedByUserId)
            .Include(c => c.SubTask)
                .ThenInclude(st => st.AssignedToUserId)
            .Include(c => c.WrittenByUserId)
            .FirstAsync(c => c.Id == id);
    }

    public async Task<List<CommentEntity>> GetAllAsync()
    {
        return await _context.Comments
            .Include(c => c.Task)
                .ThenInclude(t => t.CreatedByUserId)
            .Include(c => c.Task)
                .ThenInclude(t => t.Workspace)
                    .ThenInclude(w => w.CreatedByUserId)
            .Include(c => c.Task)
                .ThenInclude(t => t.AssignedToUserId)
            .Include(c => c.SubTask)
                .ThenInclude(st => st.Task)
                    .ThenInclude(t => t.CreatedByUserId)
            .Include(c => c.SubTask)
                .ThenInclude(st => st.Task)
                    .ThenInclude(t => t.Workspace)
                        .ThenInclude(w => w.CreatedByUserId)
            .Include(c => c.SubTask)
                .ThenInclude(st => st.Task)
                    .ThenInclude(t => t.AssignedToUserId)
            .Include(c => c.SubTask)
                .ThenInclude(st => st.CreatedByUserId)
            .Include(c => c.SubTask)
                .ThenInclude(st => st.AssignedToUserId)
            .Include(c => c.WrittenByUserId)
            .ToListAsync();
    }

    public async Task UpdateAsync(CommentEntity commentEntity)
    {
        _context.Comments.Update(commentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        CommentEntity commentEntity = await _context.Comments.FirstAsync(c => c.Id == id);
        _context.Comments.Remove(commentEntity);
        await _context.SaveChangesAsync();
    }
}
