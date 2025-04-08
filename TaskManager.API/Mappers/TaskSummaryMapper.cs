using BusinessLogic.Models;
using DataAccess.Entities;
using Task = BusinessLogic.Models.Task;

public class TaskSummaryMapper
{
    public TaskSummary Map(TaskEntity entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new TaskSummary
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Name = entity.Name,
            DueTime = entity.DueDate,
            Status = entity.Status,
            Estimate = entity.Estimate,
            Type = entity.Type,
            Priority = entity.Priority,
        };
    }
}