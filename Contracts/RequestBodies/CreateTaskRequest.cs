using Contracts.Enums;

namespace Contracts.RequestBodies;

public class CreateTaskRequest
{
    public required string Name { get; set; }
    public required string CreatedByUserEmail { get; set; }
    public required Guid WorkspaceId { get; set; }
    public StatusEnum? Status { get; set; }
    public EstimateEnum? Estimate { get; set; }
    public TypeEnum? Type { get; set; }
    public PriorityEnum? Priority { get; set; }
}
