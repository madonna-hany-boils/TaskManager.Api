using Tasky;
using Tasky.Models;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Models;

namespace TaskManager.Api.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext appDbContext;

        public TaskRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task AddTaskAsync(TaskItem item)
        {
            await appDbContext.Tasks.AddAsync(item);
        }

        public Task DeleteTaskAsync(TaskItem item)
        {
            appDbContext.Tasks.Remove(item);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await appDbContext.Tasks.ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await appDbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await appDbContext.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskItem item)
        {
            var taskToUpdate = await appDbContext.Tasks.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (taskToUpdate != null)
            {
                taskToUpdate.Title = item.Title;
                taskToUpdate.Description = item.Description;
                taskToUpdate.UpdatedAt = DateTime.UtcNow;
            }
        }
        public async Task<bool> MarkAsDoneAsync(int id)
        {
            var task = await appDbContext.Tasks.FindAsync(id);
            if (task == null) return false;

            task.Status = Status.Completed;
            await appDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<TaskItem>> GetByStatusAsync(Status status)
        {
            return await appDbContext.Tasks.Where(t => t.Status == status).ToListAsync();
        }

    }
}