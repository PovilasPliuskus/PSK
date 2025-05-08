namespace Contracts.RequestBodies;

public class CreateWorkspaceRequest
{
    public required string Name { get; set; }
    public required string CreatedByUserEmail { get; set; }
}