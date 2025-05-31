namespace Contracts.Models;

public class Comment : BaseModel
{
    public required bool Edited { get; set; }
    public string? Text { get; set; }
    public required Guid TaskId { get; set; }
    public required string CreatedByUserEmail { get; set; }
}
