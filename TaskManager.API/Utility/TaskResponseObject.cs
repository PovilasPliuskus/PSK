using BusinessLogic.Enums;

public class TaskResponseObject
{
    public required Guid Id { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }

    public required string Name { get; set; }
    public DateTime? DueDate { get; set; }
    public required Guid CreatedByUserId { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public required Guid WorkspaceId { get; set; }
    public string? Description { get; set; }
    public required StatusEnum Status { get; set; }
    public required EstimateEnum Estimate { get; set; }
    public required TypeEnum Type { get; set; }
    public required PriorityEnum Priority { get; set; }
}