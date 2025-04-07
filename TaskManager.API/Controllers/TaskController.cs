using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;
using BusinessLogic.Enums;

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
    public IActionResult GetTasks(Guid workspaceId, [FromQuery] TaskRequestDto requestDto, [FromQuery] int pageNumber, [FromQuery] int pageSize) {
        if(!ModelState.IsValid){
            // TODO update exception
            throw new Exception("Invalid request model");
        }

        

        List<TaskEntity> tasks = taskservice.getTasksFromWorkspace(workspaceId, requestDto, pageNumber, pageSize);

        return Ok(tasks);
    }
}
