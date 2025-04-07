using BusinessLogic.Enums;

public class TaskRequestDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public Guid? CreatedByUserId { get; set; }
    public DateTime? DueDateBefore { get; set; }
    public DateTime? DueDateAfter { get; set; }
    public StatusEnum? Status { get; set; }
    public PriorityEnum? Priority { get; set; }
    public EstimateEnum? Estimate { get; set; }
    public TypeEnum? Type { get; set; }
}