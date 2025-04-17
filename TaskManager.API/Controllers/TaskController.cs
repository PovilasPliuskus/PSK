using BusinessLogic.Interfaces;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskRequest request)
    {
        await _taskService.CreateTaskAsync(request);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskAsync(Guid id)
    {
        await _taskService.DeleteTaskAsync(id);

        return Ok();
    }

    [HttpGet("{workspaceId}")]
    public async Task<IActionResult> GetWorkspaceTasksAsync(Guid workspaceId)
    {
        GetWorkspaceTasksResponse response = await _taskService.GetWorkspaceTasksAsync(workspaceId);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTaskAsync([FromBody] UpdateTaskRequest request)
    {
        await _taskService.UpdateTaskAsync(request);

        return Ok();
    }
}
