using Contracts.Models;
using Contracts.Pagination;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Repositories.Interfaces;

public interface IWorkspaceRepository
{
    Task<PaginatedResult<WorkspaceWithoutTasks>> GetRangeAsync(int pageNumber, int pageSize, string userEmail);
    Task<Workspace> GetSingleAsync(Guid id);
    Task<PaginatedResult<WorkspaceUser>> GetUsersInWorkspaceAsync(int pageNumber, int pageSize, Guid workspaceId, string userEmail);
    Task AddAsync(WorkspaceWithoutTasks workspace);
    Task UpdateAsync(WorkspaceWithoutTasks workspace);
    Task RemoveAsync(Guid id);
}