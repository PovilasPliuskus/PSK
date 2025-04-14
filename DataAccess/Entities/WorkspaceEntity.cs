using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("Workspace")]
public class WorkspaceEntity : BaseModelEntity
{
    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public required string FkCreatedByUserEmail { get; set; }

    public ICollection<TaskEntity>? Tasks { get; set; }
}
