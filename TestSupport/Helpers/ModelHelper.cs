using Contracts.Models;
using Contracts.Enums;

namespace TestSupport.Helpers;

public class ModelHelper
{
    public static Attachment GetDefaultAttachmentModel(Guid id)
    {
        return new Attachment
        {
            Id = id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            FileName = "Something"
        };
    }

    public static Comment GetDefaultCommentModel(Guid id)
    {
        return new Comment
        {
            Id = id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Edited = false,
            Text = "Lorem ipsum"
        };
    }

    public static SubTask GetDefaultSubTaskModel(Guid id, Comment? comment = null,
        Attachment? attachment = null)
    {
        return new SubTask
        {
            Id = id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Name = "SubTask name",
            Status = StatusEnum.Todo,
            Estimate = EstimateEnum.Medium,
            Type = TypeEnum.Feature,
            Priority = PriorityEnum.High,
            Comments = comment as ICollection<Comment>,
            Attachments = attachment as ICollection<Attachment>
        };
    }

    public static Contracts.Models.Task GetDefaultTaskModel(Guid id, Guid workspaceId,
        string createdByUserEmail, SubTask? subTask = null, Comment? comment = null,
        Attachment? attachment = null)
    {
        return new Contracts.Models.Task
        {
            Id = id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Name = "Task name",
            DueDate = DateTime.UtcNow,
            CreatedByUserEmail = createdByUserEmail,
            AssignedToUserEmail = "JohnDoe@mail.com",
            WorkspaceId = workspaceId,
            Description = "Some task description",
            Status = StatusEnum.Todo,
            Estimate = EstimateEnum.Medium,
            Type = TypeEnum.Feature,
            Priority = PriorityEnum.High,
            SubTasks = subTask as ICollection<SubTask>,
            Comments = comment as ICollection<Comment>,
            Attachments = attachment as ICollection<Attachment>
        };
    }

    public static Workspace GetDefaultWorkspaceModel(Guid id, Contracts.Models.Task? task = null)
    {
        return new Workspace
        {
            Id = id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Name = "Workspace name",
            Tasks = task as ICollection<Contracts.Models.Task>
        };
    }
}
