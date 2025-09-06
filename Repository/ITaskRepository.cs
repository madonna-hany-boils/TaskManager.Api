using TaskManager.Api.Models;
using Tasky.Models;

namespace TaskManager.Api.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem> GetByIdAsync(int id);
        Task AddTaskAsync(TaskItem item);    
        Task UpdateTaskAsync(TaskItem item);
        Task DeleteTaskAsync(TaskItem item);
        Task<IEnumerable<TaskItem>> GetByStatusAsync(Status status);
        Task<bool> MarkAsDoneAsync(int id);
        Task SaveChangesAsync();
    }
}
