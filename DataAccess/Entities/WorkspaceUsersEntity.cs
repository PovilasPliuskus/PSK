using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("WorkspaceUsers")]
public class WorkspaceUsersEntity
{
    [Key]
    [EmailAddress]
    [StringLength(255)]
    public required string FkUserEmail { get; set; }

    [Key]
    public required Guid FkWorkspaceId { get; set; }
}
