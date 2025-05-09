using Contracts.RequestBodies;
using Contracts.ResponseBodies;

namespace BusinessLogic.Interfaces;

public interface ISubTaskService{
    Task<GetTaskSubTasksResponse> GetTaskSubTasksAsync(Guid taskId);
    Task CreateSubTaskAsync(CreateSubTaskRequest request);
    Task DeleteSubTaskAsync(Guid id);
    Task<GetSubTaskResponse> GetSubTaskAsync(Guid id);
    Task UpdateSubTaskAsync(UpdateSubTaskRequest request);
}