using BusinessLogic.Models;
using DataAccess.Entities;

public interface ITaskService
{
    List<BusinessLogic.Models.Task> GetTasksFromWorkspace(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize);
    BusinessLogic.Models.Task UpdateTask(Guid taskId, TaskDto task);
    BusinessLogic.Models.Task CreateTask(TaskDto dto, Guid workspaceId);
    void DeleteTask(Guid taskId);
}