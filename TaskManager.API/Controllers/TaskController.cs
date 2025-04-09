using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Models;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private ITaskService taskservice;
    public TaskController(ITaskService _taskService)
    {
     taskservice = _taskService;
    }

    // TODO decide between workspaceId being path or body parramater
    [HttpGet("{workspaceId}")]
    public IActionResult GetTasks(Guid workspaceId, [FromQuery] TaskRequestDto requestDto, [FromQuery] int pageNumber, [FromQuery] int pageSize) {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        List<BusinessLogic.Models.Task> tasks = taskservice.GetTasksFromWorkspace(workspaceId, requestDto, pageNumber, pageSize);
        return Ok(tasks);
    }

    [HttpPatch]
    public IActionResult UpdateTask([FromBody] BusinessLogic.Models.Task task)
    {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        BusinessLogic.Models.Task updatedTask = taskservice.UpdateTask(task);
        return Ok(updatedTask);
    }

    [HttpPost("{workspaceId}")]
    public IActionResult CreateTask(Guid workspaceId, [FromBody] BusinessLogic.Models.Task task)
    {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        BusinessLogic.Models.Task createdTask = taskservice.CreateTask(task, workspaceId);
        return Ok(createdTask);
    }

    [HttpDelete]
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
