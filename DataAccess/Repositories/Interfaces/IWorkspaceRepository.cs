using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces;

public interface IWorkspaceRepository
{
    Task CreateAsync(WorkspaceEntity workspaceEntity);
    Task<WorkspaceEntity> GetAsync(Guid id);
    Task<List<WorkspaceEntity>> GetAllAsync();
    Task UpdateAsync(WorkspaceEntity workspaceEntity);
    Task DeleteAsync(Guid id);
}
