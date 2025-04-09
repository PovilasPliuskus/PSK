using DataAccess.Entities;

public class WorkspaceService : IWorkspaceService
{
    IWorkspaceRepository workspaceRepository;

    public WorkspaceService(IWorkspaceRepository _workspaceRepository)
    {
        workspaceRepository = _workspaceRepository;
    }

    public bool DoesWorkspaceExist(Guid workspaceId)
    {
        if (workspaceRepository.GetWorkspace(workspaceId) == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}