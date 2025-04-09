using BusinessLogic.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;


public class TaskService : ITaskService
{
    private ITaskRepository taskRepository;
    private IWorkspaceService workspaceService;

    public TaskService(ITaskRepository _taskRepository, IWorkspaceService _workspaceService)
    {
        taskRepository = _taskRepository;
        workspaceService = _workspaceService;
    }

    public List<BusinessLogic.Models.Task> GetTasksFromWorkspace(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize)
    {
        if (workspaceService.DoesWorkspaceExist(workspaceId) == false)
        {
            //TODO update exception
            throw new Exception("Workplace does not exist");
        }
        
        IQueryable<TaskEntity> tasks = taskRepository.GetTasks(workspaceId, requestDto, pageNumber, pageSize);
        List<BusinessLogic.Models.Task> _tasks = new List<BusinessLogic.Models.Task>();
        foreach(TaskEntity task in tasks)
        {
            _tasks.Add(CreateTaskModelFromEntity(task));
        }
        
        return _tasks;
    }
    
    public BusinessLogic.Models.Task UpdateTask(BusinessLogic.Models.Task task)
    {
        TaskEntity retrievedTask = taskRepository.GetTask(task.Id);

        retrievedTask.Name = task.Name;
        retrievedTask.DueDate = task.DueDate;
        retrievedTask.FkAssignedToUserId = task.AssignedToUserId;
        retrievedTask.Description = task.Description;
        retrievedTask.Status = task.Status;
        retrievedTask.Estimate = task.Estimate;
        retrievedTask.Type = task.Type;
        retrievedTask.Priority = task.Priority;
        retrievedTask.UpdatedAt = DateTime.UtcNow;

        if(taskRepository.UpdateTask(retrievedTask) == 0)
        {
            //TODO update exception
            throw new Exception("Task was not updated");
        }
        
        // return updated task
        TaskEntity updatedTask = taskRepository.GetTask(task.Id);
        return CreateTaskModelFromEntity(updatedTask);
    }

    public BusinessLogic.Models.Task CreateTask(TaskCreateDto dto, Guid workspaceId)
    {
        BusinessLogic.Models.Task newtaskModel = CreateTaskModelFromDto(dto, workspaceId);
        TaskEntity newTaskEntity = CreateTaskEntityFromModel(newtaskModel);

        if(taskRepository.AddTask(newTaskEntity) == 0)
        {
            //TODO update exception
            throw new Exception("Task was not created");
        }

        TaskEntity retrievedTask = taskRepository.GetTask(newTaskEntity.Id);
        return CreateTaskModelFromEntity(retrievedTask);
    }

    public void DeleteTask(Guid taskId)
    {
        TaskEntity taskToBeDeleted = taskRepository.GetTask(taskId);
        if(taskRepository.RemoveTask(taskToBeDeleted) == 0)
        {
            //TODO update exception
            throw new Exception("Task was not deleted");
        }
    }

    private BusinessLogic.Models.Task CreateTaskModelFromDto(TaskCreateDto dto, Guid workspaceId)
    {
        return new BusinessLogic.Models.Task{
            Id = new Guid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Name = dto.Name,
            DueDate = dto.DueDate,
            Description = dto.Description,
            Status = dto.Status,
            Estimate = dto.Estimate,
            Type = dto.Type,
            Priority = dto.Priority,
            CreatedByUserId = new Guid(), // TODO: get user id from claims
            AssignedToUserId = dto.AssignedToUserId,
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