using Microsoft.EntityFrameworkCore;
using ToDo.UI.Const;
using ToDo.UI.Data;
using ToDo.UI.DTOs.TaskDto;
using ToDo.UI.DTOs.TodoDto;
using ToDo.UI.Models;

namespace ToDo.UI.Service
{
    public class ToDoService : IToDoService
    {
        private readonly TodoDbContext _context;
        public ToDoService(TodoDbContext todoDbContext)
        {
            _context = todoDbContext ??
                throw new ArgumentNullException(nameof(todoDbContext));
        }
     
        public async Task<IEnumerable<ReadToDoDto>> GetAllTodosAsync()
        {
            var todos = await _context.Todos
                .Include(t => t.Tasks)
                .ToListAsync();

            var todoDtos = todos.Select(t => new ReadToDoDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                ToDoStatus = t.ToDoStatus,
                CreatedAt = t.CreatedAt,
                Progress = t.Tasks.Any() 
                    ? Math.Round((double)t.Tasks.Count(task => task.TaskStatus == TasksStatus.Completed) / t.Tasks.Count() * 100, 2)
                    : 0,
                Tasks = t.Tasks.Select(task => new ReadTaskDto
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    TasksStatus = task.TaskStatus,
                    DurationInMinutes = task.DurationInMinutes,
                    TodoId = task.TodoId
                }).ToList()
            }).ToList();

            if (todoDtos == null || !todoDtos.Any())
            {
                return new List<ReadToDoDto>();
            }
            return todoDtos;
        }

        public async Task<ReadToDoDto> GetTodoByIdAsync(int id)
        {
            var todo = await _context.Todos
                .Include(t => t.Tasks)
                .FirstOrDefaultAsync(t => t.Id == id);
            
            if (todo == null)
            {
                return null;
            }
            var todoDto = new ReadToDoDto()
            {
                Id = todo.Id,
                Name = todo.Name,
                Description = todo.Description,
                ToDoStatus = todo.ToDoStatus,
                CreatedAt = todo.CreatedAt,
                Progress = todo.Progress
            };
                return todoDto;
        }
        public async Task<ReadToDoDto> CreateTodoAsync(CreateToDoDto todo)
        {
            var newTodo = new Todos
            {
                Name = todo.Name,
                Description = todo.Description,
                ToDoStatus = todo.ToDoStatus,
                CreatedAt = todo.CreatedAt,
            };
            await _context.Todos.AddAsync(newTodo);
            await _context.SaveChangesAsync();
            var todoDto = new ReadToDoDto
            {
                Id = newTodo.Id,
                Name = newTodo.Name,
                Description = newTodo.Description,
                ToDoStatus = newTodo.ToDoStatus,
                CreatedAt = newTodo.CreatedAt,
                Progress = newTodo.Progress
            };
            return todoDto;
        }
        public async Task<ReadToDoDto> UpdateTodoAsync(int id, UpdateToDoDto todo) 
        {
            var existingTodo = await _context.Todos
                .FirstOrDefaultAsync(t => t.Id == id);
            if (existingTodo == null)
            {
                return null;
            }
            existingTodo.Name = todo.Name?? existingTodo.Name;
            existingTodo.Description = todo.Description ?? existingTodo.Description;
            existingTodo.ToDoStatus = todo.ToDoStatus != default ? todo.ToDoStatus : existingTodo.ToDoStatus;

            await _context.SaveChangesAsync();
            var todoDto = new ReadToDoDto
            {
                Id = existingTodo.Id,
                Name = existingTodo.Name,
                Description = existingTodo.Description,
                ToDoStatus = existingTodo.ToDoStatus,
                CreatedAt = existingTodo.CreatedAt,
                Progress = existingTodo.Progress
            };
            return todoDto;
        }
        public async Task<bool> DeleteTodoAsync(int id)
        {
            var result = await _context.Todos
                .FirstOrDefaultAsync(t => t.Id == id);
            if (result == null)
            {
                return false;
            }
            _context.Todos.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
