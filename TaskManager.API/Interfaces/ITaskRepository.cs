using DataAccess.Entities;

public interface ITaskRepository
{
    IQueryable<TaskEntity> GetTasks(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize);
    TaskEntity GetTask(Guid Id);
    int UpdateTask(TaskEntity updatedTask);
    int AddTask(TaskEntity task);
    int RemoveTask(TaskEntity task);
}