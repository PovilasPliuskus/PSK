﻿namespace Contracts.Models;

public class Workspace : BaseModel
{
    public required string Name { get; set; }
    public required string CreatedByUserEmail { get; set; }
    public ICollection<Task>? Tasks { get; set; }
}
