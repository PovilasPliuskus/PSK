using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class UserEntity
{
    [Key]
    [EmailAddress]
    [StringLength(255)]
    public required string Email { get; set; }

    [Required]
    [StringLength(255)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(255)]
    public required string LastName { get; set; }
}
