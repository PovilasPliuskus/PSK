namespace Contracts.RequestBodies;

public class UpdateWorkspaceRequest
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string CreatedByUserEmail { get; set; }
    public required uint Version { get; set; }
    public bool Force { get; set; } = false;
}