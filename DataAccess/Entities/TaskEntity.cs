using Contracts.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("Task")]
public class TaskEntity : BaseModelEntity
{
    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public required string FkCreatedByUserEmail { get; set; }

    [Required]
    public required Guid FkWorkspaceId { get; set; }

    [ForeignKey(nameof(FkWorkspaceId))]
    public WorkspaceEntity? Workspace { get; set; }

    [EmailAddress]
    [StringLength(255)]
    public string? FkAssignedToUserEmail { get; set; }

    public DateTime? DueDate { get; set; }

    [StringLength(5000)]
    public string? Description { get; set; }

    [Required]
    public required StatusEnum Status { get; set; }

    [Required]
    public required EstimateEnum Estimate { get; set; }

    [Required]
    public required TypeEnum Type { get; set; }

    [Required]
    public required PriorityEnum Priority { get; set; }

    public ICollection<SubTaskEntity>? SubTasks { get; set; }
    public ICollection<CommentEntity>? Comments { get; set; }
    public ICollection<AttachmentEntity>? Attachments { get; set; }
}
