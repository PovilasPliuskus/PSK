using BusinessLogic.Models;
using DataAccess.Entities;

public interface ITaskService
{
    List<BusinessLogic.Models.Task> GetTasksFromWorkspace(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize);
    List<TaskSummary> GetTaskSummariesFromWorkspace(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize);
}