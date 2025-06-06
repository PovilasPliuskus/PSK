using Contracts.RequestBodies;
using Contracts.ResponseBodies;

namespace BusinessLogic.Interfaces;

public interface IWorkspaceService
{
    Task<GetWorkspacesResponse> GetWorkspacePageAsync(int pageNumber, int pageSize, string userEmail);
    Task<GetWorkspaceResponse> GetWorkspaceByIdAsync(Guid workspaceId);
    Task<GetUsersInWorkspaceResponse> GetUsersInWorkspaceAsync(int pageNumber, int pageSize, Guid workspaceId, string userEmail);
    Task AddUserToWorkspaceAsync(Guid workspaceId, AddUserRequest request);
    Task RemoveUserFromWorkspaceAsync(Guid workspaceId, RemoveUserRequest request);
    Task CreateWorkspaceAsync(CreateWorkspaceRequest request);
    Task UpdateWorkspaceAsync(UpdateWorkspaceRequest request);
    Task DeleteWorkspaceAsync(Guid id);
}