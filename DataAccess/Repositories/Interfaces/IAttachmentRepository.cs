using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces;

public interface IAttachmentRepository
{
    Task CreateAsync(AttachmentEntity attachmentEntity);
    Task<AttachmentEntity> GetAsync(Guid id);
    Task<List<AttachmentEntity>> GetAllAsync();
    Task UpdateAsync(AttachmentEntity attachmentEntity);
    Task DeleteAsync(Guid id);
}
