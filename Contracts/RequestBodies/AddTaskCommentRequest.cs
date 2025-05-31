namespace Contracts.RequestBodies;

public class AddTaskCommentRequest
{
    public required Guid TaskId { get; set; }
    public required string CommentText { get; set; }
    public string? CreatedByUserEmail { get; set; }
}