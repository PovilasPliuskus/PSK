using BusinessLogic.Interfaces;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;
using DataAccess.Extensions;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Exceptions;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubTaskController : ControllerBase{
    private readonly ISubTaskService _subTaskService;

    public SubTaskController(ISubTaskService subTaskService)
    {
        _subTaskService = subTaskService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubTaskAsync([FromBody] CreateSubTaskRequest request)
    {
        var userEmail = User.GetUserEmail();

        if (string.IsNullOrEmpty(userEmail))
        {
            throw new NotAuthenticatedException("User is not authenticated");
        }
        request.CreatedByUserEmail = userEmail;
        
        await _subTaskService.CreateSubTaskAsync(request);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskAsync(Guid id)
    {
        await _subTaskService.DeleteSubTaskAsync(id);

        return Ok();
    }

    [HttpGet("{taskId}")]
    public async Task<IActionResult> GetTaskSubTasksAsync(Guid taskId)
    {
        GetTaskSubTasksResponse response = await _subTaskService.GetTaskSubTasksAsync(taskId);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSubTaskAsync([FromBody] UpdateSubTaskRequest request)
    {
        await _subTaskService.UpdateSubTaskAsync(request);

        return Ok();
    }
}