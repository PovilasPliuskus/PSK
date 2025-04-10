using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private ITaskService taskservice;
    public TaskController(ITaskService _taskService)
    {
     taskservice = _taskService;
    }

    [HttpGet("{workspaceId}")]
    [Authorize]
    public async Task<IActionResult> GetTasksAsync(Guid workspaceId, [FromQuery] TaskRequestDto requestDto, [FromQuery] int pageNumber, [FromQuery] int pageSize) {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        List<BusinessLogic.Models.Task> tasks = await taskservice.GetTasksFromWorkspaceAsync(workspaceId, requestDto, pageNumber, pageSize);
        return Ok(tasks);
    }

    [HttpPatch("{taskId}")]
    [Authorize]
    public async Task<IActionResult> UpdateTaskAsync(Guid taskId, [FromBody] TaskDto taskDto)
    {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        BusinessLogic.Models.Task updatedTask = await taskservice.UpdateTaskAsync(taskId, taskDto);
        return Ok(updatedTask);
    }

    [HttpPost("{workspaceId}")]
    [Authorize]
    public async Task<IActionResult> CreateTaskAsync(Guid workspaceId, [FromBody] TaskDto taskDto)
    {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        BusinessLogic.Models.Task createdTask = await taskservice.CreateTaskAsync(taskDto, workspaceId);
        return Ok(createdTask);
    }

    [HttpDelete("{taskId}")]
    [Authorize]
    public async Task<IActionResult> DeleteTaskAsync(Guid taskId)
    {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        await taskservice.DeleteTaskAsync(taskId);

        return Ok();
    }
}
