using Model = Contracts.Models;

namespace DataAccess.Repositories.Interfaces;

public interface ISubTaskRepository{
    Task<List<Model.SubTask>> GetTaskSubtasks(Guid taskId);
    Task<Model.SubTask> GetAsync(Guid id);
    Task AddAsync(Model.SubTask subTask);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(Model.SubTask task);
}