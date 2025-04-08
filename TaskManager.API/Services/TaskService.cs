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

    // Sito endpoint galimai nereikia, nes tikriausiai nereikes vienu metu fetchint keletos task objektu (vietoj to fetchinam task summary)
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

    public List<TaskSummary> GetTaskSummariesFromWorkspace(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize)
    {
        if (workspaceService.DoesWorkspaceExist(workspaceId) == false)
        {
            //TODO update exception
            throw new Exception("Workplace does not exist");
        }

        // TODO make this injectable
        TaskSummaryMapper mapper = new TaskSummaryMapper();

        IQueryable<TaskEntity> tasks = taskRepository.GetTasks(workspaceId, requestDto, pageNumber, pageSize);
        List<TaskSummary> taskSummaries = new List<TaskSummary>();
        foreach(TaskEntity task in tasks)
        {
            taskSummaries.Add(mapper.Map(task));
        }
        return taskSummaries;
    }
}