namespace Contracts.ResponseBodies;

public class GetWorkspaceTasksResponse
{
    public List<GetTaskResponse>? Tasks { get; init; }
}
