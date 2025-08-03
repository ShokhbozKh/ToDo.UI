using ToDo.UI.DTOs.TodoDto;
using ToDo.UI.Models;

namespace ToDo.UI.Service;

public interface IToDoService
{
    Task<IEnumerable<ReadToDoDto>> GetAllTodosAsync();
    Task<ReadToDoDto> GetTodoByIdAsync(int id);
    Task<ReadToDoDto> CreateTodoAsync(CreateToDoDto todo);
    Task<ReadToDoDto> UpdateTodoAsync(int id, UpdateToDoDto todo);
    Task<bool> DeleteTodoAsync(int id);
}
