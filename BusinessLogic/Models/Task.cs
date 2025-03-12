﻿using BusinessLogic.Enums;

namespace BusinessLogic.Models;

public class Task : BaseModel
{
    public required string Name { get; set; }
    public DateTime? DueTime { get; set; }
    public string? Description { get; set; }
    public required StatusEnum Status { get; set; }
    public required EstimateEnum Estimate { get; set; }
    public required TypeEnum Type { get; set; }
    public required PriorityEnum Priority { get; set; }
    public ICollection<SubTask>? SubTasks { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Attachment>? Attachments { get; set; }
}
