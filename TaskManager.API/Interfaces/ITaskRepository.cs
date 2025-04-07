using DataAccess.Entities;

public interface ITaskRepository
{
    IQueryable<TaskEntity> getTasks(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize);
}