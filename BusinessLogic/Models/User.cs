namespace BusinessLogic.Models;

public class User : BaseModel
{
    public required string StudentId { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public ICollection<Workspace>? Workspaces { get; set; }
    public ICollection<Task>? Tasks { get; set; }
    public ICollection<SubTask>? SubTasks { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Attachment>? Attachments { get; set; }
}
