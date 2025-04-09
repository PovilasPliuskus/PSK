using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;
using BusinessLogic.Enums;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    [HttpGet("{workspaceId}")]
    public IActionResult GetTasks(Guid workspaceId)
    {
        // Create some dummy TaskEntity data
        var tasks = new List<TaskEntity>
            {
                new TaskEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Implement User Authentication",
                    FkCreatedByUserId = Guid.NewGuid(),
                    FkWorkspaceId = workspaceId,
                    FkAssignedToUserId = Guid.NewGuid(),
                    DueDate = DateTime.Now.AddDays(7),
                    Description = "Implement user login and registration functionality.",
                    Status = StatusEnum.InProgress,
                    Estimate = EstimateEnum.Medium,
                    Type = TypeEnum.Feature,
                    Priority = PriorityEnum.High
                },
                new TaskEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Design Task List UI",
                    FkCreatedByUserId = Guid.NewGuid(),
                    FkWorkspaceId = workspaceId,
                    FkAssignedToUserId = null, // Not assigned yet
                    DueDate = DateTime.Now.AddDays(3),
                    Description = "Design the user interface for displaying the list of tasks.",
                    Status = StatusEnum.InProgress,
                    Estimate = EstimateEnum.Small,
                    Type = TypeEnum.Bug,
                    Priority = PriorityEnum.Medium
                },
                new TaskEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Fix Bug in Task Editing",
                    FkCreatedByUserId = Guid.NewGuid(),
                    FkWorkspaceId = workspaceId,
                    FkAssignedToUserId = Guid.NewGuid(),
                    DueDate = DateTime.Now.AddDays(1),
                    Description = "Investigate and fix the bug where task editing is not saving correctly.",
                    Status = StatusEnum.InProgress,
                    Estimate = EstimateEnum.Small,
                    Type = TypeEnum.Bug,
                    Priority = PriorityEnum.High
                }
            };

        // Filter the dummy data by the provided workspaceId
        var tasksForWorkspace = tasks.Where(t => t.FkWorkspaceId == workspaceId).ToList();

        // Return the dummy data as an Ok result
        return Ok(tasksForWorkspace);
    }
}
