using DataAccess.Context;
using DataAccess.Entities;

public class WorkspaceRepository : IWorkspaceRepository
{
    private TaskManagerContext dbContext;
    public WorkspaceRepository(TaskManagerContext _dbContext)
    {
        dbContext = _dbContext;
    }

    public WorkspaceEntity GetWorkspace(Guid workspaceId)
    {
        return dbContext.Workspaces.Where(w => w.Id == workspaceId).FirstOrDefault();
    }
}