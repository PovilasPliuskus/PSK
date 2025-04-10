using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces;

public interface ITaskRepository
{
    Task CreateAsync(TaskEntity taskEntity);
    Task<TaskEntity> GetAsync(Guid id);
    Task<List<TaskEntity>> GetAllAsync();
    Task UpdateAsync(TaskEntity taskEntity);
    Task DeleteAsync(Guid id);
}
