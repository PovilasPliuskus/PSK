using BusinessLogic.Enums;

public class TaskDto
{
    public required string Name { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public string? Description { get; set; }
    public required StatusEnum Status { get; set; }
    public required EstimateEnum Estimate { get; set; }
    public required TypeEnum Type { get; set; }
    public required PriorityEnum Priority { get; set; }
}