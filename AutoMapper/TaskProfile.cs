using AutoMapper;
using TaskManager.Api.DTOs;
using Tasky.Models;

namespace TaskManager.Api.AutoMapper
{
    public class TaskProfile:Profile
    {
        public TaskProfile() {
            CreateMap< TaskCreateDTO, TaskItem>();
            CreateMap< TaskUpdateDTO, TaskItem>();
            CreateMap<TaskItem, TaskResponseDTO>();

        
        }
    }
}
