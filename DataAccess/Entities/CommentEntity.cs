using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class CommentEntity : BaseModelEntity
{
    public Guid? FkTaskId { get; set; }

    [ForeignKey(nameof(FkTaskId))]
    public TaskEntity? Task { get; set; }

    public Guid? FkSubTaskId { get; set; }

    [ForeignKey(nameof(FkSubTaskId))]
    public SubTaskEntity? SubTask { get; set; }

    [Required]
    public required Guid FkWrittenByUserId { get; set; }

    [ForeignKey(nameof(FkWrittenByUserId))]
    public UserEntity? WrittenByUserId { get; set; }

    [Required]
    public required bool Edited { get; set; } = false;

    [StringLength(1000)]
    public string? Text { get; set; }
}
