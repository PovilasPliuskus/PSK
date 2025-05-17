namespace Contracts.RequestBodies;

public class RemoveUserRequest
{
    public required string UserEmail { get; set; }
}