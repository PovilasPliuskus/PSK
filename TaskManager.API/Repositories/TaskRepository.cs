using DataAccess.Context;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

public class TaskRepository : ITaskRepository
{
    private TaskManagerContext dbContext;
    public TaskRepository(TaskManagerContext _dbContext)
    {
        dbContext = _dbContext;
    }

    public async Task<List<TaskEntity>> GetTasksAsync(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize)
    {
        IQueryable<TaskEntity> query = dbContext.Tasks.Where(t => t.FkWorkspaceId == workspaceId);
        if (requestDto.Id.HasValue)
        {
            query = query.Where(t => t.Id == requestDto.Id.Value);
        }

        if (!string.IsNullOrEmpty(requestDto.Name))
        {
            query = query.Where(t => t.Name.Contains(requestDto.Name));
        }

        if (requestDto.AssignedToUserId.HasValue)
        {
            query = query.Where(t => t.FkAssignedToUserId == requestDto.AssignedToUserId.Value);
        }

        if (requestDto.CreatedByUserId.HasValue)
        {
            query = query.Where(t => t.FkCreatedByUserId == requestDto.CreatedByUserId.Value);
        }

        if (requestDto.DueDateBefore.HasValue)
        {
            query = query.Where(t => t.DueDate <= requestDto.DueDateBefore.Value);
        }

        if (requestDto.DueDateAfter.HasValue)
        {
            query = query.Where(t => t.DueDate >= requestDto.DueDateAfter.Value);
        }

        if (requestDto.Status.HasValue)
        {
            query = query.Where(t => t.Status == requestDto.Status.Value);
        }

        if (requestDto.Priority.HasValue)
        {
            query = query.Where(t => t.Priority == requestDto.Priority.Value);
        }

        if (requestDto.Estimate.HasValue)
        {
            query = query.Where(t => t.Estimate == requestDto.Estimate.Value);
        }

        if (requestDto.Type.HasValue)
        {
            query = query.Where(t => t.Type == requestDto.Type.Value);
        }

        return await query
            .OrderBy(t => t.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<TaskEntity> GetTaskAsync(Guid Id)
    {
        TaskEntity task = await dbContext.Tasks.FindAsync(Id);

        if(task == null)
        {
            //TODO update exception
            throw new Exception("Task does not exist");
        }
        return task;
    }

    public async Task<int> UpdateTaskAsync(TaskEntity updatedTask)
    {
        dbContext.Entry(updatedTask).State = EntityState.Modified;

        // returns rowsChanged
        return await dbContext.SaveChangesAsync();
    }

    public async Task<int> AddTaskAsync(TaskEntity task)
    {
        await dbContext.Tasks.AddAsync(task);

        // returns rowsChanged
        return await dbContext.SaveChangesAsync();
    }

    public async Task<int> RemoveTaskAsync(TaskEntity task)
    {
        TaskEntity taskToBeRemoved = await dbContext.Tasks.FindAsync(task.Id);
        if(taskToBeRemoved == null)
        {
            //TODO update exception
            throw new Exception("Task does not exist");
        }
        dbContext.Tasks.Remove(taskToBeRemoved);

        // returns rowsChanged
        return await dbContext.SaveChangesAsync();
    }
}