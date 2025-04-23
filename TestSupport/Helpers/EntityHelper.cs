using Contracts.Enums;
using DataAccess.Entities;

namespace TestSupport.Helpers;

public class EntityHelper
{
    public static TaskEntity GetDefaultTaskEntity(Guid workspaceId, string createdByUserEmail)
    {
        return new TaskEntity
        {
            Name = "Task name",
            DueDate = DateTime.UtcNow,
            FkCreatedByUserEmail = createdByUserEmail,
            FkAssignedToUserEmail = "JohnDoe@mail.com",
            FkWorkspaceId = workspaceId,
            Description = "Some task description",
            Status = StatusEnum.Todo,
            Estimate = EstimateEnum.Medium,
            Type = TypeEnum.Feature,
            Priority = PriorityEnum.High,
        };
    }
}
