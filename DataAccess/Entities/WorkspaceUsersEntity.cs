using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("WorkspaceUsers")]
public class WorkspaceUsersEntity
{
    [Key]
    public required Guid FkUserId { get; set; }

    [Key]
    public required Guid FkWorkspaceId { get; set; }
}
