using AutoMapper;
using Contracts.Models;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model = Contracts.Models;
using Threading = System.Threading.Tasks;

namespace DataAccess.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagerContext _context;
    private readonly IMapper _mapper;

    public TaskRepository(TaskManagerContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Model.Task>> GetAllFromWorkspaceAsync(Guid workspaceId)
    {
        List<TaskEntity> taskEntities = await _context.Tasks
            .Where(t => t.FkWorkspaceId == workspaceId)
            .Include(t => t.SubTasks)
                .ThenInclude(st => st.Comments)
            .Include(t => t.SubTasks)
                .ThenInclude(st => st.Attachments)
            .Include(t => t.Comments)
            .Include(t => t.Attachments)
            .ToListAsync();

        return _mapper.Map<List<Model.Task>>(taskEntities);
    }

    public async Task<Model.Task> GetAsync(Guid id)
    {
        TaskEntity taskEntity = await _context.Tasks
            .Include(t => t.SubTasks)
                .ThenInclude(st => st.Comments)
            .Include(t => t.SubTasks)
                .ThenInclude(st => st.Attachments)
            .Include(t => t.Comments)
            .Include(t => t.Attachments)
            .FirstAsync(t => t.Id == id);

        return _mapper.Map<Model.Task>(taskEntity);
    }

    public async Threading.Task AddAsync(Model.Task task)
    {
        TaskEntity taskEntity = _mapper.Map<TaskEntity>(task);

        await _context.Tasks.AddAsync(taskEntity);

        await _context.SaveChangesAsync();
    }

    public async Threading.Task RemoveAsync(Guid id)
    {
        TaskEntity taskEntity = await _context.Tasks
            .Include(t => t.SubTasks)
                .ThenInclude(st => st.Comments)
            .Include(t => t.SubTasks)
                .ThenInclude(st => st.Attachments)
            .Include(t => t.Comments)
            .Include(t => t.Attachments)
            .FirstAsync(t => t.Id == id);

        _context.Tasks.Remove(taskEntity);

        await _context.SaveChangesAsync();
    }
}
