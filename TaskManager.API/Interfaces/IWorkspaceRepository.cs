using DataAccess.Entities;

public interface IWorkspaceRepository
{
    WorkspaceEntity getWorkspace(Guid workspaceId);
}