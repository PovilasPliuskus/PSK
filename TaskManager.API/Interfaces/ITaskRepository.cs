using DataAccess.Entities;

public interface ITaskRepository
{
    Task<List<TaskEntity>> GetTasksAsync(Guid workspaceId, TaskQueryObject queryObject, int pageNumber, int pageSize);
    Task<TaskEntity> GetTaskAsync(Guid Id);
    Task<int> UpdateTaskAsync(TaskEntity updatedTask);
    Task<int> AddTaskAsync(TaskEntity task);
    Task<int> RemoveTaskAsync(TaskEntity task);
}