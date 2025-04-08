using DataAccess.Entities;

public interface ITaskService
{
    List<TaskEntity> GetTasksFromWorkspace(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize);
}