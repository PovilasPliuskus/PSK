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

    public static WorkspaceEntity GetDefaultWorkspaceEntity(Guid workspaceId, string createdByUserEmail)
    {
        var taskEntity = GetDefaultTaskEntity(workspaceId, createdByUserEmail);

        return new WorkspaceEntity
        {
            Name = "Workspace name",
            FkCreatedByUserEmail = createdByUserEmail,
            Tasks = new List<TaskEntity>
            {
                taskEntity
            }
        };
    }
}
