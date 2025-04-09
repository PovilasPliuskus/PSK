using BusinessLogic.Models;
using DataAccess.Entities;

public interface ITaskService
{
    List<BusinessLogic.Models.Task> GetTasksFromWorkspace(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize);
    BusinessLogic.Models.Task UpdateTask(BusinessLogic.Models.Task task);
    BusinessLogic.Models.Task CreateTask(TaskCreateDto dto, Guid workspaceId);
    void DeleteTask(Guid taskId);
}