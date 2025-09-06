using System.ComponentModel.DataAnnotations;

namespace Tasky.Models;

public class User
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Username { get; set; } = default!;

    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    // Password will be hashed (e.g. BCrypt)
    [Required]
    public string PasswordHash { get; set; } = default!;

    public string Role { get; set; } = "User"; // Default role

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Property
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
