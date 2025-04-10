using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces;

public interface ICommentRepository
{
    Task CreateAsync(CommentEntity commentEntity);
    Task<CommentEntity> GetAsync(Guid id);
    Task<List<CommentEntity>> GetAllAsync();
    Task UpdateAsync(CommentEntity commentEntity);
    Task DeleteAsync(Guid id);
}
