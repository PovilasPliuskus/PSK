using Contracts.RequestBodies;
using Contracts.ResponseBodies;

namespace BusinessLogic.Interfaces;

public interface IWorkspaceService
{
    Task<GetWorkspacesResponse> GetWorkspacePageAsync(int pageNumber, int pageSize, string userEmail);
    Task<GetWorkspaceResponse> GetWorkspaceByIdAsync(Guid workspaceId);
    Task CreateWorkspaceAsync(CreateWorkspaceRequest request);
    Task UpdateWorkspaceAsync(UpdateWorkspaceRequest request);
    Task DeleteWorkspaceAsync(Guid id);
}