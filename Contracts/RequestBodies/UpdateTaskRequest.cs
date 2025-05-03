using Contracts.Enums;

namespace Contracts.RequestBodies;

public class UpdateTaskRequest
{
    public required Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime? DueDate { get; set; }
    public string? AssignedToUserEmail { get; set; }
    public string? Description { get; set; }
    public StatusEnum? Status { get; set; }
    public EstimateEnum? Estimate { get; set; }
    public TypeEnum? Type { get; set; }
    public PriorityEnum? Priority { get; set; }
    public required uint Version { get; set; }
    public bool Force { get; set; } = false;
}
