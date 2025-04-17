using Model = Contracts.Models;

namespace DataAccess.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<List<Model.Task>> GetAllFromWorkspaceAsync(Guid workspaceId);
    Task<Model.Task> GetAsync(Guid id);
    Task AddAsync(Model.Task task);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(Model.Task task);
}
