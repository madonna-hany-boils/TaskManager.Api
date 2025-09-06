using TaskManager.Api.Models;

namespace TaskManager.Api.DTOs
{
    public class TaskUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
    }
}
