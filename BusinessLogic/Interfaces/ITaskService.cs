using Contracts.RequestBodies;
using Contracts.ResponseBodies;

namespace BusinessLogic.Interfaces
{
    public interface ITaskService
    {
        Task CreateTaskAsync(CreateTaskRequest request);
        Task DeleteTaskAsync(Guid id);
        Task<GetWorkspaceTasksResponse> GetWorkspaceTasks(Guid workspaceId);
    }
}
