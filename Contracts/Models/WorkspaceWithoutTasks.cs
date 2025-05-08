namespace Contracts.Models;

public class WorkspaceWithoutTasks : BaseModel
{
    public required string Name { get; set; }
    public required string CreatedByUserEmail { get; set; }
    public bool Force { get; set; } = false;
}