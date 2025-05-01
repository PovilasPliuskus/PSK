using Contracts.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IWorkspaceRepository
{
    Task<List<WorkspaceWithoutTasks>> GetAllAsync();
    Task<Workspace> GetSingleAsync(Guid id);
    /*Task AddAsync(WorkspaceWithoutTasks workspace);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(WorkspaceWithoutTasks workspace);*/
}