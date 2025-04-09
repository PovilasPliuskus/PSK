using BusinessLogic.Models;
using DataAccess.Entities;
using Task = BusinessLogic.Models.Task;

public class TaskMapper
{
    public Task Map(TaskEntity entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new Task
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Name = entity.Name,
            DueDate = entity.DueDate,
            Description = entity.Description,
            Status = entity.Status,
            Estimate = entity.Estimate,
            Type = entity.Type,
            Priority = entity.Priority,
            CreatedByUserId = entity.FkCreatedByUserId,
            AssignedToUserId = entity.FkAssignedToUserId,
            // TODO finish this
            // ...
        };
    }
}