using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Entities;

[Table("Comment")]
public class CommentEntity : BaseModelEntity
{
    public Guid? FkTaskId { get; set; }

    [ForeignKey(nameof(FkTaskId))]
    [JsonIgnore]
    public TaskEntity? Task { get; set; }

    public Guid? FkSubTaskId { get; set; }

    [ForeignKey(nameof(FkSubTaskId))]
    [JsonIgnore]
    public SubTaskEntity? SubTask { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public required string FkWrittenByUserEmail { get; set; }

    [Required]
    public required bool Edited { get; set; } = false;

    [StringLength(1000)]
    public string? Text { get; set; }
}
