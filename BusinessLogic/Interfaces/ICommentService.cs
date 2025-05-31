using Contracts.RequestBodies;
using Contracts.ResponseBodies;

namespace BusinessLogic.Interfaces;

public interface ICommentService
{
    Task<GetTaskCommentsResponse> GetTaskCommentsAsync(Guid taskId);
    Task<GetTaskCommentResponse> GetTaskCommentAsync(Guid commentId);
    Task AddTaskCommentAsync(AddTaskCommentRequest request);
    Task DeleteTaskCommentAsync(Guid commentId);
}