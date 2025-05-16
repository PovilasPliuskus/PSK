namespace Contracts.Models;

public class WorkspaceUser : BaseModel
{
    public required string UserEmail { get; set; }
    public required bool IsOwner { get; set; }
}