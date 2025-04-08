using BusinessLogic.Enums;
using BusinessLogic.Models;

public class TaskSummary : BaseModel
{
    public required string Name { get; set; }
    public required StatusEnum Status { get; set; }
}