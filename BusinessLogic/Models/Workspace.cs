namespace BusinessLogic.Models;

public class Workspace : BaseModel
{
    public required string Name { get; set; }
    public ICollection<Task>? Tasks { get; set; }
}
