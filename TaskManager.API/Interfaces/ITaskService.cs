using DataAccess.Entities;

public interface ITaskService
{
    List<TaskEntity> getTasksFromWorkspace(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize);
}