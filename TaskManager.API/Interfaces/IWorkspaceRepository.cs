using DataAccess.Entities;

public interface IWorkspaceRepository
{
    WorkspaceEntity GetWorkspace(Guid workspaceId);
}