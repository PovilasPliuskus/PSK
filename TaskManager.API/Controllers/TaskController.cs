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
    public IActionResult GetTasks(Guid workspaceId, [FromQuery] TaskRequestDto requestDto, [FromQuery] int pageNumber, [FromQuery] int pageSize) {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        List<BusinessLogic.Models.Task> tasks = taskservice.GetTasksFromWorkspace(workspaceId, requestDto, pageNumber, pageSize);
        return Ok(tasks);
    }

    [HttpPatch("{taskId}")]
    [Authorize]
    public IActionResult UpdateTask(Guid taskId, [FromBody] TaskDto taskDto)
    {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        BusinessLogic.Models.Task updatedTask = taskservice.UpdateTask(taskId, taskDto);
        return Ok(updatedTask);
    }

    [HttpPost("{workspaceId}")]
    [Authorize]
    public IActionResult CreateTask(Guid workspaceId, [FromBody] TaskDto taskDto)
    {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        BusinessLogic.Models.Task createdTask = taskservice.CreateTask(taskDto, workspaceId);
        return Ok(createdTask);
    }

    [HttpDelete("{taskId}")]
    [Authorize]
    public IActionResult DeleteTask(Guid taskId)
    {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        taskservice.DeleteTask(taskId);

        return Ok();
    }
}
