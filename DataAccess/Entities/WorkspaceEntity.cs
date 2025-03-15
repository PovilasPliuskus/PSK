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
    public required Guid FkCreatedByUserId { get; set; }

    [ForeignKey(nameof(FkCreatedByUserId))]
    public UserEntity? CreatedByUserId { get; set; }
}
