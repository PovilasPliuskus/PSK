using DataAccess.Entities;

public class WorkspaceService : IWorkspaceService
{
    IWorkspaceRepository workspaceRepository;

    public WorkspaceService(IWorkspaceRepository _workspaceRepository)
    {
        workspaceRepository = _workspaceRepository;
    }

    public async Task<bool> DoesWorkspaceExistAsync(Guid workspaceId)
    {
        if (await workspaceRepository.GetWorkspaceAsync(workspaceId) == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}