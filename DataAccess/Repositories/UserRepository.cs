using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TaskManagerContext _context;

    public UserRepository(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(UserEntity userEntity)
    {
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<UserEntity> GetAsync(Guid id)
    {
        return await _context.Users.FirstAsync(u => u.Id == id);
    }

    public async Task<List<UserEntity>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task UpdateAsync(UserEntity userEntity)
    {
        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        UserEntity userEntity = await _context.Users.FirstAsync(u => u.Id == id);
        _context.Users.Remove(userEntity);
        await _context.SaveChangesAsync();
    }
}
