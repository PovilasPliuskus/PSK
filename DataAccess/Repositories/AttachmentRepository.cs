using DataAccess.Context;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AttachmentRepository
{
    private readonly TaskManagerContext _context;

    public AttachmentRepository(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(AttachmentEntity attachmentEntity)
    {
        await _context.Attachments.AddAsync(attachmentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<AttachmentEntity> GetAsync(Guid id)
    {
        return await _context.Attachments
            .Include(a => a.CreatedByUserId)
            .Include(a => a.Task)
                .ThenInclude(t => t.CreatedByUserId)
            .Include(a => a.Task)
                .ThenInclude(t => t.Workspace)
                    .ThenInclude(w => w.CreatedByUserId)
            .Include(a => a.Task)
                .ThenInclude(t => t.AssignedToUserId)
            .Include(a => a.SubTask)
                .ThenInclude(st => st.Task)
                    .ThenInclude(t => t.CreatedByUserId)
            .Include(a => a.SubTask)
                .ThenInclude(st => st.Task)
                    .ThenInclude(t => t.Workspace)
                        .ThenInclude(w => w.CreatedByUserId)
            .Include(a => a.SubTask)
                .ThenInclude(st => st.Task)
                    .ThenInclude(t => t.AssignedToUserId)
            .Include(a => a.SubTask)
                .ThenInclude(st => st.CreatedByUserId)
            .Include(a => a.SubTask)
                .ThenInclude(st => st.AssignedToUserId)
            .FirstAsync(a => a.Id == id);
    }

    public async Task<List<AttachmentEntity>> GetAllAsync()
    {
        return await _context.Attachments
            .Include(a => a.CreatedByUserId)
            .Include(a => a.Task)
                .ThenInclude(t => t.CreatedByUserId)
            .Include(a => a.Task)
                .ThenInclude(t => t.Workspace)
                    .ThenInclude(w => w.CreatedByUserId)
            .Include(a => a.Task)
                .ThenInclude(t => t.AssignedToUserId)
            .Include(a => a.SubTask)
                .ThenInclude(st => st.Task)
                    .ThenInclude(t => t.CreatedByUserId)
            .Include(a => a.SubTask)
                .ThenInclude(st => st.Task)
                    .ThenInclude(t => t.Workspace)
                        .ThenInclude(w => w.CreatedByUserId)
            .Include(a => a.SubTask)
                .ThenInclude(st => st.Task)
                    .ThenInclude(t => t.AssignedToUserId)
            .Include(a => a.SubTask)
                .ThenInclude(st => st.CreatedByUserId)
            .Include(a => a.SubTask)
                .ThenInclude(st => st.AssignedToUserId)
            .ToListAsync();
    }

    public async Task UpdateAsync(AttachmentEntity attachmentEntity)
    {
        _context.Attachments.Update(attachmentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        AttachmentEntity attachmentEntity = await _context.Attachments.FirstAsync(a => a.Id == id);
        _context.Attachments.Remove(attachmentEntity);
        await _context.SaveChangesAsync();
    }
}
