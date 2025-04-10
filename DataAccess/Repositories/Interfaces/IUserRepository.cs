using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces;

public interface IUserRepository
{
    Task CreateAsync(UserEntity userEntity);
    Task<UserEntity> GetAsync(Guid id);
    Task<List<UserEntity>> GetAllAsync();
    Task UpdateAsync(UserEntity userEntity);
    Task DeleteAsync(Guid id);
}
