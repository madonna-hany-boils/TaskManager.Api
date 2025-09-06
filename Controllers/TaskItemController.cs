using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.DTOs;
using TaskManager.Api.Models;
using TaskManager.Api.Repository;
using Tasky.Models;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskItemController(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        private int getUserId()
        {
            return int.Parse(User.FindFirst("sub")!.Value);
        }

        // GET: api/TaskItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDTO>>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllAsync();
            var taskDtos = _mapper.Map<IEnumerable<TaskResponseDTO>>(tasks);
            return Ok(taskDtos);
        }

        // GET: api/TaskItem/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponseDTO>> GetTaskById(int id)
        {
            var taskItem = await _taskRepository.GetByIdAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            var taskDto = _mapper.Map<TaskResponseDTO>(taskItem);
            return Ok(taskDto);
        }

        // POST: api/TaskItem
        [HttpPost]
        public async Task<ActionResult<TaskResponseDTO>> AddTaskItem([FromBody]TaskCreateDTO taskCreateDto)
        {
            if (taskCreateDto == null)
            {
                return BadRequest("Task item cannot be null.");
            }

            var taskEntity = _mapper.Map<TaskItem>(taskCreateDto);

            await _taskRepository.AddTaskAsync(taskEntity);
            await _taskRepository.SaveChangesAsync();

            var taskReadDto = _mapper.Map<TaskResponseDTO>(taskEntity);

            return CreatedAtAction(nameof(GetTaskById), new { id = taskEntity.Id }, taskReadDto);
        }

        // PUT: api/TaskItem/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(int id, TaskUpdateDTO taskUpdateDto)
        {
            var taskEntity = await _taskRepository.GetByIdAsync(id);
            if (taskEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(taskUpdateDto, taskEntity);

            await _taskRepository.UpdateTaskAsync(taskEntity);
            await _taskRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/TaskItem/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var taskItem = await _taskRepository.GetByIdAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            await _taskRepository.DeleteTaskAsync(taskItem);
            await _taskRepository.SaveChangesAsync();

            return NoContent();
        }

        // ✅ Mark as Done
        [HttpPatch("{id}/done")]
        public async Task<IActionResult> MarkAsDone(int id)
        {
            var result = await _taskRepository.MarkAsDoneAsync(id);
            if (!result) return NotFound();

            return Ok("Task is Marked Done");
        }


        // GET: api/TaskItem/filter?status=Completed
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<TaskResponseDTO>>> GetTasksByStatus([FromQuery] string status)
        {
            if (string.IsNullOrEmpty(status))
                return BadRequest("Status is required. Use: Pending or Completed.");

            if (!Enum.TryParse<Status>(status, true, out var parsedStatus))
                return BadRequest("Invalid status. Use: Pending or Completed.");

            var tasks = await _taskRepository.GetByStatusAsync(parsedStatus);

            var taskDtos = _mapper.Map<IEnumerable<TaskResponseDTO>>(tasks);
            return Ok(taskDtos);
        }


    }
}
