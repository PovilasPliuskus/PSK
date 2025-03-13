using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class WorkspaceUsersEntity
{
    [Key]
    public required Guid FkUserId { get; set; }

    [Key]
    public required Guid FkWorkspaceId { get; set; }
}
