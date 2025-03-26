using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("Attachment")]
public class AttachmentEntity : BaseModelEntity
{
    [Required]
    [StringLength(255)]
    public required string FileName { get; set; }

    [Required]
    public required Guid FkCreatedByUserId { get; set; }

    [ForeignKey(nameof(FkCreatedByUserId))]
    public UserEntity? CreatedByUserId { get; set; }

    public Guid? FkTaskId { get; set; }

    [ForeignKey(nameof(FkTaskId))]
    public TaskEntity? Task { get; set; }

    public Guid? FkSubTaskId { get; set; }

    [ForeignKey(nameof(FkSubTaskId))]
    public SubTaskEntity? SubTask { get; set; }
}
