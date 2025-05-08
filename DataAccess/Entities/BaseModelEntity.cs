using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public abstract class BaseModelEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    [Timestamp]
    public uint Version { get; set; }
}
