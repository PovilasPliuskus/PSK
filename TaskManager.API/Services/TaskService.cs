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

    public List<TaskEntity> getTasksFromWorkspace(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize)
    {
        if (workspaceService.DoesWorkspaceExist(workspaceId) == false)
        {
            //TODO update exception
            throw new Exception("Workplace does not exist");
        }

        IQueryable<TaskEntity> tasks = taskRepository.getTasks(workspaceId, requestDto, pageNumber, pageSize);
        return tasks.ToList();
    }
}