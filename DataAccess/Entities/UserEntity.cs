using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class UserEntity : BaseModelEntity
{
    [Required]
    [StringLength(7)]
    public required string StudentId { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public required string Email { get; set; }

    [Required]
    [StringLength(255)]
    public required string PasswordHash { get; set; }

    [Required]
    [StringLength(255)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(255)]
    public required string LastName { get; set; }
}
