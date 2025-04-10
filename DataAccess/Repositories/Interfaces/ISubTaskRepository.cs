using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces;

public interface ISubTaskRepository
{
    Task CreateAsync(SubTaskEntity subTaskEntity);
    Task<SubTaskEntity> GetAsync(Guid id);
    Task<List<SubTaskEntity>> GetAllAsync();
    Task UpdateAsync(SubTaskEntity subTaskEntity);
    Task DeleteAsync(Guid id);
}
