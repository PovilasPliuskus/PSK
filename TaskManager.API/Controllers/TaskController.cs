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

    [HttpGet("{workspaceId}")]
    public IActionResult GetTaskSummaries(Guid workspaceId, [FromQuery] TaskRequestDto requestDto, [FromQuery] int pageNumber, [FromQuery] int pageSize) {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        List<BusinessLogic.Models.TaskSummary> tasks = taskservice.GetTaskSummariesFromWorkspace(workspaceId, requestDto, pageNumber, pageSize);
        return Ok(tasks);
    }
}
