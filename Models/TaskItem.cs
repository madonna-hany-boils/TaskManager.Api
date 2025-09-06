
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Api.Models;

namespace Tasky.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = default!;

    [MaxLength(1000)]
    public string? Description { get; set; }
    public Status Status { get; set; } = Status.Pending;


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    // owner
    public int UserId { get; set; }
    public User? User { get; set; }
}
