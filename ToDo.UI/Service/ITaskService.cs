using ToDo.UI.DTOs.TaskDto;

namespace ToDo.UI.Service
{
    public interface ITaskService
    {
        Task<IEnumerable<ReadTaskDto>> GetAllTasksAsync();
        Task<ReadTaskDto> GetTaskByIdAsync(int id);
        Task<ReadTaskDto> CreateTaskAsync(CreateTaskDto task);
        Task<ReadTaskDto> UpdateTaskAsync(int id, UpdateTaskDto task);
        Task<bool> DeleteTaskAsync(int id);

    }
}
