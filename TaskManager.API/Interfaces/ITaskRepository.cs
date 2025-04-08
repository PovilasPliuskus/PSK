using DataAccess.Entities;

public interface ITaskRepository
{
    IQueryable<TaskEntity> GetTasks(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize);
}