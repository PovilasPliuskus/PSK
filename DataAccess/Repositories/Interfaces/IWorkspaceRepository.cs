using Contracts.Models;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Repositories.Interfaces;

public interface IWorkspaceRepository
{
    Task<List<WorkspaceWithoutTasks>> GetAllAsync();
    Task<Workspace> GetSingleAsync(Guid id);
    Task AddAsync(WorkspaceWithoutTasks workspace);
    Task UpdateAsync(WorkspaceWithoutTasks workspace);
    /*Task RemoveAsync(Guid id);*/
}