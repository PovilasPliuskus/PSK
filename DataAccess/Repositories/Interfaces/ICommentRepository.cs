using Contracts.Models;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetRangeAsync(Guid taskId);
    Task<Comment> GetSingleAsync(Guid commentId);
    Task AddAsync(Comment comment);
    Task RemoveAsync(Guid commentId);
}