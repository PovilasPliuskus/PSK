using BusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class TaskEntity : BaseModelEntity
{
    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    [Required]
    public required Guid FkCreatedByUserId { get; set; }

    [ForeignKey(nameof(FkCreatedByUserId))]
    public UserEntity? CreatedByUserId { get; set; }

    [Required]
    public required Guid FkWorkspaceId { get; set; }

    [ForeignKey(nameof(FkWorkspaceId))]
    public WorkspaceEntity? Workspace { get; set; }

    public Guid? FkAssignedToUserId { get; set; }

    [ForeignKey(nameof(FkAssignedToUserId))]
    public UserEntity? AssignedToUserId { get; set; }

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
}
