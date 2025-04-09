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
        
        //TODO make this injectable
        TaskMapper mapper = new TaskMapper();

        IQueryable<TaskEntity> tasks = taskRepository.GetTasks(workspaceId, requestDto, pageNumber, pageSize);
        List<BusinessLogic.Models.Task> _tasks = new List<BusinessLogic.Models.Task>();
        foreach(TaskEntity task in tasks)
        {
            _tasks.Add(mapper.Map(task));
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
        // TODO update updatedAt field

        if(taskRepository.UpdateTask(retrievedTask) == 0)
        {
            //TODO update exception
            throw new Exception("Task was not updated");
        }
        
        // return updated task
        TaskEntity updatedTask = taskRepository.GetTask(task.Id);

        //TODO make this injectable
        TaskMapper mapper = new TaskMapper();
        return mapper.Map(updatedTask);
    }

    public BusinessLogic.Models.Task CreateTask(BusinessLogic.Models.Task task, Guid workspaceId)
    {
        TaskEntity newtask = new TaskEntity{
            Id = new Guid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Name = task.Name,
            FkCreatedByUserId = new Guid(), // TODO get the user id from the claim
            FkWorkspaceId = workspaceId,
            FkAssignedToUserId = task.AssignedToUserId,
            DueDate = task.DueDate,
            Description = task.Description,
            Status = task.Status,
            Estimate = task.Estimate,
            Type = task.Type,
            Priority = task.Priority
        };

        if(taskRepository.AddTask(newtask) == 0)
        {
            //TODO update exception
            throw new Exception("Task was not created");
        }

        TaskEntity retrievedTask = taskRepository.GetTask(newtask.Id);

        TaskMapper mapper = new TaskMapper();
        return mapper.Map(retrievedTask);
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
}