using DataAccess.Entities;
using BusinessLogic.Enums;

namespace TestSupport.Helpers;

public class EntityHelper
{
    public static UserEntity GetDefaultUserEntity()
    {
        return new UserEntity
        {
            StudentId = "1234567",
            Email = "JohnSmith@mail.com",
            PasswordHash = "asdasdasd",
            FirstName = "John",
            LastName = "Smith"
        };
    }

    public static WorkspaceEntity GetDefaultWorkspaceEntity(UserEntity createdByUserEntity)
    {
        return new WorkspaceEntity
        {
            Name = "Amazing Name",
            FkCreatedByUserId = createdByUserEntity.Id,
            CreatedByUserId = createdByUserEntity
        };
    }

    public static TaskEntity GetDefaultTaskEntity(UserEntity createdByUserEntity, WorkspaceEntity workspaceEntity, UserEntity assignedToUserEntity)
    {
        return new TaskEntity
        {
            Name = "Code",
            FkCreatedByUserId = createdByUserEntity.Id,
            CreatedByUserId = createdByUserEntity,
            FkWorkspaceId = workspaceEntity.Id,
            Workspace = workspaceEntity,
            FkAssignedToUserId = assignedToUserEntity.Id,
            AssignedToUserId = assignedToUserEntity,
            DueDate = DateTime.Now,
            Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
            Status = StatusEnum.Todo,
            Estimate = EstimateEnum.Medium,
            Type = TypeEnum.Bug,
            Priority = PriorityEnum.High
        };
    }

    public static SubTaskEntity GetDefaultSubTaskEntity(TaskEntity taskEntity, UserEntity createdByUserEntity, UserEntity assignedToUserEntity)
    {
        return new SubTaskEntity
        {
            Name = "Code",
            FkTaskId = taskEntity.Id,
            Task = taskEntity,
            FkCreatedByUserId = createdByUserEntity.Id,
            CreatedByUserId = createdByUserEntity,
            FkAssignedToUserId = assignedToUserEntity.Id,
            AssignedToUserId= assignedToUserEntity,
            DueDate = DateTime.Now,
            Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
            Status = StatusEnum.Todo,
            Estimate = EstimateEnum.Medium,
            Type = TypeEnum.Bug,
            Priority = PriorityEnum.High
        };
    }

    public static AttachmentEntity GetDefaultAttachmentEntity(UserEntity createdByUserEntity, TaskEntity taskEntity, SubTaskEntity subTaskEntity)
    {
        return new AttachmentEntity
        {
            FileName = "EntityHelper",
            FkCreatedByUserId = createdByUserEntity.Id,
            CreatedByUserId = createdByUserEntity,
            FkTaskId = taskEntity.Id,
            Task = taskEntity,
            FkSubTaskId = subTaskEntity.Id,
            SubTask = subTaskEntity
        };
    }

    public static CommentEntity GetDefaultCommentEntity(TaskEntity taskEntity, SubTaskEntity subTaskEntity, UserEntity writtenByUserEntity)
    {
        return new CommentEntity
        {
            FkTaskId = taskEntity.Id,
            Task = taskEntity,
            FkSubTaskId = subTaskEntity.Id,
            SubTask = subTaskEntity,
            FkWrittenByUserId = writtenByUserEntity.Id,
            WrittenByUserId = writtenByUserEntity,
            Edited = false,
            Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry."
        };
    }
}
