namespace Contracts.ResponseBodies;

public class GetTaskCommentsResponse
{
    public List<GetTaskCommentResponse>? Comments { get; init; }
}