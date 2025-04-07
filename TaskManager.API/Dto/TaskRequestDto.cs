using BusinessLogic.Enums;

public class TaskRequestDto
{
    public Guid? id { get; set; }
    public string? name { get; set; }
    public Guid? assignedToUserId { get; set; }
    public Guid? createdByUserId { get; set; }
    public DateTime? dueDateBefore { get; set; }
    public DateTime? dueDateAfter { get; set; }
    public StatusEnum? status { get; set; }
    public PriorityEnum? priority { get; set; }
    public EstimateEnum? estimate { get; set; }
    public TypeEnum? type { get; set; }
}