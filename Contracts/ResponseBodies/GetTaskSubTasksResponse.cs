namespace Contracts.ResponseBodies;

public class GetTaskSubTasksResponse
{
    public List<GetSubTaskResponse>? SubTasks { get; init; }
}
