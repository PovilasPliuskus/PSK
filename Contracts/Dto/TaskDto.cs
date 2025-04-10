using Contracts.Enums;
using Dto.Contracts;

namespace Dto.Constracts;

public class TaskDto : BaseDto
{
    public string? Name { get; set; }

    public Guid? CreatedByUserId { get; set; }

    public Guid? WorkspaceId { get; set; }

    public Guid? AssignedToUserId { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Description { get; set; }

    public StatusEnum? Status { get; set; }

    public EstimateEnum? Estimate { get; set; }

    public TypeEnum? Type { get; set; }

    public PriorityEnum? Priority { get; set; }
}
