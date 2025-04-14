namespace Contracts.Models;

public class Comment : BaseModel
{
    public required bool Edited { get; set; }
    public string? Text { get; set; }
}
