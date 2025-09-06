using TaskManager.Api.Models;

namespace TaskManager.Api.DTOs
{
    public class TaskResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Status  Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UserId { get; set; }
    }
}
