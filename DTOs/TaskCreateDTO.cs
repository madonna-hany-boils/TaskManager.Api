namespace TaskManager.Api.DTOs
{
    public class TaskCreateDTO
    {
      
        public string Title { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }

    }
}
