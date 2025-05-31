using AutoMapper;
using BusinessLogic.Interfaces;
using Contracts.Models;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;
using DataAccess.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace BusinessLogic.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }
    
    public async Task<GetTaskCommentsResponse> GetTaskCommentsAsync(Guid taskId)
    {
        var taskComments = await _commentRepository.GetRangeAsync(taskId);
        
        return _mapper.Map<GetTaskCommentsResponse>(taskComments);
    }

    public async Task<GetTaskCommentResponse> GetTaskCommentAsync(Guid commentId)
    {
        var comment = await _commentRepository.GetSingleAsync(commentId);
        
        return _mapper.Map<GetTaskCommentResponse>(comment);
    }

    public async Task AddTaskCommentAsync(AddTaskCommentRequest request)
    {
        var taskComment = _mapper.Map<Comment>(request);

        await _commentRepository.AddAsync(taskComment);
    }

    public async Task DeleteTaskCommentAsync(Guid commentId)
    {
        await _commentRepository.RemoveAsync(commentId);
    }
}