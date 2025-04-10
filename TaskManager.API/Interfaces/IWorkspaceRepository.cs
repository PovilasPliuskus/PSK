using DataAccess.Entities;

public interface IWorkspaceRepository
{
    Task<WorkspaceEntity> GetWorkspaceAsync(Guid workspaceId);
}