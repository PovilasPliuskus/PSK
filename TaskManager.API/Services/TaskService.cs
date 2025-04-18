using System.Security.Claims;
using BusinessLogic.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;


public class TaskService : ITaskService
{
    private ITaskRepository taskRepository;
    private IWorkspaceService workspaceService;
    private IHttpContextAccessor httpContextAccessor;

    public TaskService(ITaskRepository _taskRepository, IWorkspaceService _workspaceService, IHttpContextAccessor _httpContextAccessor)
    {
        taskRepository = _taskRepository;
        workspaceService = _workspaceService;
        httpContextAccessor = _httpContextAccessor;
    }

    public async Task<List<BusinessLogic.Models.Task>> GetTasksFromWorkspaceAsync(Guid workspaceId, TaskQueryObject queryObject, int pageNumber, int pageSize)
    {
        if (await workspaceService.DoesWorkspaceExistAsync(workspaceId) == false)
        {
            //TODO update exception
            throw new Exception("Workplace does not exist");
        }
        
        List<TaskEntity> tasks = await taskRepository.GetTasksAsync(workspaceId, queryObject, pageNumber, pageSize);
        List<BusinessLogic.Models.Task> _tasks = new List<BusinessLogic.Models.Task>();
        foreach(TaskEntity task in tasks)
        {
            _tasks.Add(CreateTaskModelFromEntity(task));
        }
        
        return _tasks;
    }
    
    public async Task<BusinessLogic.Models.Task> UpdateTaskAsync(Guid taskId, TaskRequestObject requestObject)
    {
        TaskEntity retrievedTask = await taskRepository.GetTaskAsync(taskId);

        retrievedTask.Name = requestObject.Name;
        retrievedTask.DueDate = requestObject.DueDate;
        retrievedTask.FkAssignedToUserId = requestObject.AssignedToUserId;
        retrievedTask.Description = requestObject.Description;
        retrievedTask.Status = requestObject.Status;
        retrievedTask.Estimate = requestObject.Estimate;
        retrievedTask.Type = requestObject.Type;
        retrievedTask.Priority = requestObject.Priority;
        retrievedTask.UpdatedAt = DateTime.UtcNow;

        if(await taskRepository.UpdateTaskAsync(retrievedTask) == 0)
        {
            //TODO update exception
            throw new Exception("Task was not updated");
        }
        
        // return updated task
        TaskEntity updatedTask = await taskRepository.GetTaskAsync(taskId);
        return CreateTaskModelFromEntity(updatedTask);
    }

    public async Task<BusinessLogic.Models.Task> CreateTaskAsync(TaskRequestObject requestObject, Guid workspaceId)
    {
        // get user id from claims
        var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        if(userIdClaim == null)
        {
            //TODO update exception
            throw new Exception("Failed to get user id from claims");
        }
        
        BusinessLogic.Models.Task newtaskModel = CreateTaskModelFromRequestObject(requestObject, workspaceId, Guid.Parse(userIdClaim.Value));
        TaskEntity newTaskEntity = CreateTaskEntityFromModel(newtaskModel);

        if(await taskRepository.AddTaskAsync(newTaskEntity) == 0)
        {
            //TODO update exception
            throw new Exception("Task was not created");
        }

        TaskEntity retrievedTask = await taskRepository.GetTaskAsync(newTaskEntity.Id);
        
        return CreateTaskModelFromEntity(retrievedTask);
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(Guid taskId)
    {
        TaskEntity taskToBeDeleted = await taskRepository.GetTaskAsync(taskId);
        if(await taskRepository.RemoveTaskAsync(taskToBeDeleted) == 0)
        {
            //TODO update exception
            throw new Exception("Task was not deleted");
        }
    }

    private BusinessLogic.Models.Task CreateTaskModelFromRequestObject(TaskRequestObject requestObject, Guid workspaceId, Guid userId)
    {

        return new BusinessLogic.Models.Task{
            Id = new Guid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Name = requestObject.Name,
            DueDate = requestObject.DueDate,
            Description = requestObject.Description,
            Status = requestObject.Status,
            Estimate = requestObject.Estimate,
            Type = requestObject.Type,
            Priority = requestObject.Priority,
            //CreatedByUserId = userId,
            CreatedByUserId = Guid.Parse("8388f8cb-760b-4e42-8f2e-d0f01ece0757"), // temporarly use this while user is not saved to db.
            AssignedToUserId = requestObject.AssignedToUserId,
            WorkspaceId = workspaceId
        };
    }

    private BusinessLogic.Models.Task CreateTaskModelFromEntity(TaskEntity entity)
    {
        return new BusinessLogic.Models.Task
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Name = entity.Name,
            DueDate = entity.DueDate,
            Description = entity.Description,
            Status = entity.Status,
            Estimate = entity.Estimate,
            Type = entity.Type,
            Priority = entity.Priority,
            CreatedByUserId = entity.FkCreatedByUserId,
            AssignedToUserId = entity.FkAssignedToUserId,
            WorkspaceId = entity.FkWorkspaceId
        };
    }

    private TaskEntity CreateTaskEntityFromModel(BusinessLogic.Models.Task model)
    {
        return new TaskEntity{
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
            FkCreatedByUserId = model.CreatedByUserId,
            FkAssignedToUserId = model.AssignedToUserId,
            FkWorkspaceId = model.WorkspaceId
        };
    }
}