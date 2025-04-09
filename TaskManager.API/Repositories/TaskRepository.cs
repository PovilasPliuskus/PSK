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

    public IQueryable<TaskEntity> GetTasks(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize)
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

        return query
            .OrderBy(t => t.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public TaskEntity GetTask(Guid Id)
    {
        TaskEntity task = dbContext.Tasks.Find(Id);

        if(task == null)
        {
            //TODO update exception
            throw new Exception("Task does not exist");
        }
        return task;
    }

    public int UpdateTask(TaskEntity updatedTask)
    {
        dbContext.Entry(updatedTask).State = EntityState.Modified;

        // returns rowsChanged
        return dbContext.SaveChanges();
    }

    public int AddTask(TaskEntity task)
    {
        dbContext.Tasks.Add(task);

        // returns rowsChanged
        return dbContext.SaveChanges();
    }

    public int RemoveTask(TaskEntity task)
    {
        TaskEntity taskToBeRemoved = dbContext.Tasks.Find(task.Id);
        if(taskToBeRemoved == null)
        {
            //TODO update exception
            throw new Exception("Task does not exist");
        }
        dbContext.Tasks.Remove(taskToBeRemoved);

        // returns rowsChanged
        return dbContext.SaveChanges();
    }
}