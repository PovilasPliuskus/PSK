using DataAccess.Context;
using DataAccess.Entities;

public class TaskRepository : ITaskRepository
{
    private TaskManagerContext dbContext;
    public TaskRepository(TaskManagerContext _dbContext)
    {
        dbContext = _dbContext;
    }

    public IQueryable<TaskEntity> getTasks(Guid workspaceId, TaskRequestDto requestDto, int pageNumber, int pageSize)
    {
        IQueryable<TaskEntity> query = dbContext.Tasks.Where(t => t.FkWorkspaceId == workspaceId);
        if (requestDto.id.HasValue)
        {
            query = query.Where(t => t.Id == requestDto.id.Value);
        }

        if (!string.IsNullOrEmpty(requestDto.name))
        {
            query = query.Where(t => t.Name.Contains(requestDto.name));
        }

        if (requestDto.assignedToUserId.HasValue)
        {
            query = query.Where(t => t.FkAssignedToUserId == requestDto.assignedToUserId.Value);
        }

        if (requestDto.createdByUserId.HasValue)
        {
            query = query.Where(t => t.FkCreatedByUserId == requestDto.createdByUserId.Value);
        }

        if (requestDto.dueDateBefore.HasValue)
        {
            query = query.Where(t => t.DueDate <= requestDto.dueDateBefore.Value);
        }

        if (requestDto.dueDateAfter.HasValue)
        {
            query = query.Where(t => t.DueDate >= requestDto.dueDateAfter.Value);
        }

        if (requestDto.status.HasValue)
        {
            query = query.Where(t => t.Status == requestDto.status.Value);
        }

        if (requestDto.priority.HasValue)
        {
            query = query.Where(t => t.Priority == requestDto.priority.Value);
        }

        if (requestDto.estimate.HasValue)
        {
            query = query.Where(t => t.Estimate == requestDto.estimate.Value);
        }

        if (requestDto.type.HasValue)
        {
            query = query.Where(t => t.Type == requestDto.type.Value);
        }

        return query
            .OrderBy(t => t.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

    }
}

// koks request dto
// koks return dto

// kur saugomi useriai
// ar tokenas grazina user id
// registracija - 