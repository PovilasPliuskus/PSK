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

    public async Task<List<TaskEntity>> GetTasksAsync(Guid workspaceId, TaskQueryObject queryObject, int pageNumber, int pageSize)
    {
        IQueryable<TaskEntity> query = dbContext.Tasks.Where(t => t.FkWorkspaceId == workspaceId);
        if (queryObject.Id.HasValue)
        {
            query = query.Where(t => t.Id == queryObject.Id.Value);
        }

        if (!string.IsNullOrEmpty(queryObject.Name))
        {
            query = query.Where(t => t.Name.Contains(queryObject.Name));
        }

        if (queryObject.AssignedToUserId.HasValue)
        {
            query = query.Where(t => t.FkAssignedToUserId == queryObject.AssignedToUserId.Value);
        }

        if (queryObject.CreatedByUserId.HasValue)
        {
            query = query.Where(t => t.FkCreatedByUserId == queryObject.CreatedByUserId.Value);
        }

        if (queryObject.DueDateBefore.HasValue)
        {
            query = query.Where(t => t.DueDate <= queryObject.DueDateBefore.Value);
        }

        if (queryObject.DueDateAfter.HasValue)
        {
            query = query.Where(t => t.DueDate >= queryObject.DueDateAfter.Value);
        }

        if (queryObject.Status.HasValue)
        {
            query = query.Where(t => t.Status == queryObject.Status.Value);
        }

        if (queryObject.Priority.HasValue)
        {
            query = query.Where(t => t.Priority == queryObject.Priority.Value);
        }

        if (queryObject.Estimate.HasValue)
        {
            query = query.Where(t => t.Estimate == queryObject.Estimate.Value);
        }

        if (queryObject.Type.HasValue)
        {
            query = query.Where(t => t.Type == queryObject.Type.Value);
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