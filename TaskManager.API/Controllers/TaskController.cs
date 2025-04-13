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
    public async Task<IActionResult> GetTasksAsync(Guid workspaceId, [FromQuery] TaskQueryObject queryObject, [FromQuery] int pageNumber, [FromQuery] int pageSize) {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        List<BusinessLogic.Models.Task> tasks = await taskservice.GetTasksFromWorkspaceAsync(workspaceId, queryObject, pageNumber, pageSize);
        return Ok(tasks);
    }

    [HttpPatch("{taskId}")]
    [Authorize]
    public async Task<IActionResult> UpdateTaskAsync(Guid taskId, [FromBody] TaskRequestObject requestObject)
    {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        BusinessLogic.Models.Task updatedTask = await taskservice.UpdateTaskAsync(taskId, requestObject);
        TaskResponseObject response = CreateResponseObjectFromTaskModel(updatedTask);
        return Ok(response);
    }

    [HttpPost("{workspaceId}")]
    [Authorize]
    public async Task<IActionResult> CreateTaskAsync(Guid workspaceId, [FromBody] TaskRequestObject requestObject)
    {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        BusinessLogic.Models.Task createdTask = await taskservice.CreateTaskAsync(requestObject, workspaceId);
        TaskResponseObject response = CreateResponseObjectFromTaskModel(createdTask);
        return Ok(response);
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

    private TaskResponseObject CreateResponseObjectFromTaskModel(BusinessLogic.Models.Task model)
    {

        return new TaskResponseObject{
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Name = model.Name,
            DueDate = model.DueDate,
            Description = model.Description,
            Status = model.Status,
            Estimate = model.Estimate,
            Type = model.Type,
            Priority = model.Priority,
            CreatedByUserId = model.CreatedByUserId,
            AssignedToUserId = model.AssignedToUserId,
            WorkspaceId = model.WorkspaceId
        };
    }
}
