using DataAccess.Context;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

public class WorkspaceRepository : IWorkspaceRepository
{
    private TaskManagerContext dbContext;
    public WorkspaceRepository(TaskManagerContext _dbContext)
    {
        dbContext = _dbContext;
    }

    public async Task<WorkspaceEntity> GetWorkspaceAsync(Guid workspaceId)
    {
        return await dbContext.Workspaces.Where(w => w.Id == workspaceId).FirstOrDefaultAsync();
    }
}