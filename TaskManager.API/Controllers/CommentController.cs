using BusinessLogic.Interfaces;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;
using DataAccess.Extensions;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Exceptions;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }
    
    [HttpGet("task/{taskId}")]
    public async Task<IActionResult> GetTaskCommentsAsync(Guid taskId)
    {
        if (string.IsNullOrEmpty(User.GetUserEmail()))
            throw new NotAuthenticatedException("User is not authenticated");
        
        var response = await _commentService.GetTaskCommentsAsync(taskId);
        return Ok(response);
    }

    [HttpGet("{commentId}")]
    public async Task<IActionResult> GetTaskCommentAsync(Guid commentId)
    {
        if (string.IsNullOrEmpty(User.GetUserEmail()))
            throw new NotAuthenticatedException("User is not authenticated");
        
        GetTaskCommentResponse response = await _commentService.GetTaskCommentAsync(commentId);

        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTaskCommentAsync([FromBody] AddTaskCommentRequest request)
    {
        var userEmail = User.GetUserEmail();
        if (string.IsNullOrEmpty(User.GetUserEmail()))
        {
            throw new NotAuthenticatedException("User is not authenticated");
        }
        
        request.CreatedByUserEmail = userEmail;
        await _commentService.AddTaskCommentAsync(request);

        return Ok();
    }
    
    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteTaskCommentAsync(Guid commentId)
    {
        if (string.IsNullOrEmpty(User.GetUserEmail()))
            throw new NotAuthenticatedException("User is not authenticated");
        
        await _commentService.DeleteTaskCommentAsync(commentId);

        return Ok();
    }
}