using BusinessLogic.Models;
using DataAccess.Entities;

public interface ITaskService
{
    Task<List<BusinessLogic.Models.Task>> GetTasksFromWorkspaceAsync(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize);
    Task<BusinessLogic.Models.Task> UpdateTaskAsync(Guid taskId, TaskDto task);
    Task<BusinessLogic.Models.Task> CreateTaskAsync(TaskDto dto, Guid workspaceId);
    System.Threading.Tasks.Task DeleteTaskAsync(Guid taskId);
}