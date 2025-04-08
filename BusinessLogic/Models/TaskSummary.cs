using BusinessLogic.Enums;
using BusinessLogic.Models;

namespace BusinessLogic.Models;

public class TaskSummary : BaseModel
{
    public required string Name { get; set; }
    public DateTime? DueTime { get; set; }
    public required StatusEnum Status { get; set; }
    public required EstimateEnum Estimate { get; set; }
    public required TypeEnum Type { get; set; }
    public required PriorityEnum Priority { get; set; }

    public static TaskSummary FromTaskModel(BusinessLogic.Models.Task task){
        return new TaskSummary{
            Id = task.Id,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt,
            Name = task.Name,
            DueTime = task.DueTime,
            Status = task.Status,
            Estimate = task.Estimate,
            Type = task.Type,
            Priority = task.Priority,
        };
    }
    
}