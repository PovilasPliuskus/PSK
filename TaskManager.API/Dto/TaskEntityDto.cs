using BusinessLogic.Enums;

public class TaskEntityDto
{
    public string Name { get; set; }

    public Guid Workspace { get; set; }

    public Guid? FkAssignedToUserId { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Description { get; set; }

    public StatusEnum Status { get; set; }

    public EstimateEnum Estimate { get; set; }

    public TypeEnum Type { get; set; }

    public PriorityEnum Priority { get; set; }
}