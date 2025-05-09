namespace Contracts.RequestBodies;

public class CreateSubTaskRequest
{
    public required string Name { get; set; }
    public string? CreatedByUserEmail { get; set; }
    public required Guid WorkspaceId { get; set; }
    public required Guid TaskId { get; set; }
    public required uint Version { get; set; }
    public bool Force { get; set; } = false;
}