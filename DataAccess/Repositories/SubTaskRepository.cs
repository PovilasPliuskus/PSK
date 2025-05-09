using AutoMapper;
using Contracts.Models;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model = Contracts.Models;
using Threading = System.Threading.Tasks;

namespace DataAccess.Repositories;

public class SubTaskRepository : ISubTaskRepository{
    private readonly TaskManagerContext _context;
    private readonly IMapper _mapper;

    public SubTaskRepository(TaskManagerContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Model.SubTask>> GetTaskSubtasks(Guid taskId){
        List<SubTaskEntity> subTaskEntities = await _context.SubTasks
            .Where(s => s.FkTaskId == taskId)
            .Include(s => s.Comments)
            .Include(s => s.Attachments)
            .ToListAsync();

            return _mapper.Map<List<Model.SubTask>>(subTaskEntities);
    }

    public async Threading.Task<Model.SubTask> GetAsync(Guid id){
        SubTaskEntity subTaskEntity = await _context.SubTasks
            .Include(s => s.Comments)
            .Include(s => s.Attachments)
            .FirstAsync(s => s.Id == id);

        return _mapper.Map<Model.SubTask>(subTaskEntity);
    }

    public async Threading.Task AddAsync(Model.SubTask subTask){
        SubTaskEntity subTaskEntity = _mapper.Map<SubTaskEntity>(subTask);

        await _context.SubTasks.AddAsync(subTaskEntity);

        await _context.SaveChangesAsync();
    }

    public async Threading.Task RemoveAsync(Guid id){
        SubTaskEntity subTaskEntity = await _context.SubTasks
            .Include(s => s.Comments)
            .Include(s => s.Attachments)
            .FirstAsync(s => s.Id == id);

        _context.SubTasks.Remove(subTaskEntity);

        await _context.SaveChangesAsync();
    }

    public async Threading.Task UpdateAsync(Model.SubTask subTask){
        SubTaskEntity existingEntity = await _context.SubTasks
            .FirstAsync(s => s.Id == subTask.Id);

        if (!subTask.Force && existingEntity.Version != subTask.Version)
        {
            throw new DbUpdateConcurrencyException("The subTask has been modified by another user.");
        }

        existingEntity.Name = subTask.Name;
        existingEntity.DueDate = subTask.DueDate;
        existingEntity.FkAssignedToUserEmail = subTask.AssignedToUserEmail;
        existingEntity.Description = subTask.Description;
        existingEntity.Status = subTask.Status;
        existingEntity.Estimate = subTask.Estimate;
        existingEntity.Type = subTask.Type;
        existingEntity.Priority = subTask.Priority;

        _context.SubTasks.Update(existingEntity);

        await _context.SaveChangesAsync();
    }
}