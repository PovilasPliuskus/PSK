using BusinessLogic.Models;
using DataAccess.Entities;

public interface ITaskService
{
    Task<List<BusinessLogic.Models.Task>> GetTasksFromWorkspaceAsync(Guid workspaceId, TaskQueryObject queryObject, int pageNumber, int pageSize);
    Task<BusinessLogic.Models.Task> UpdateTaskAsync(Guid taskId, TaskRequestObject task);
    Task<BusinessLogic.Models.Task> CreateTaskAsync(TaskRequestObject requestObject, Guid workspaceId);
    System.Threading.Tasks.Task DeleteTaskAsync(Guid taskId);
}