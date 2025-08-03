using Microsoft.EntityFrameworkCore;
using ToDo.UI.Data;
using ToDo.UI.DTOs.TaskDto;
using ToDo.UI.Models;

namespace ToDo.UI.Service;

public class TaskService : ITaskService
{
    private readonly TodoDbContext _context;
    public TaskService(TodoDbContext todoDbContext)
    {
        _context = todoDbContext ??
            throw new ArgumentNullException(nameof(todoDbContext));
    }
    public async Task<IEnumerable<ReadTaskDto>> GetAllTasksAsync()
    {
        var tasks = await _context.Tasks
            .Include(t => t.Todo)
            .ToListAsync();
        var taskDtos = tasks.Select(t => new ReadTaskDto
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description,
            TasksStatus = t.TaskStatus,
            DurationInMinutes = t.DurationInMinutes,
            TodoId = t.TodoId
        }).ToList();
        if (taskDtos == null || !taskDtos.Any())
        {
            return new List<ReadTaskDto>();
        }
        return taskDtos;
    }

    public async Task<ReadTaskDto> GetTaskByIdAsync(int id)
    {
        var task = await _context.Tasks
            .Include(t => t.Todo)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (task == null)
        {
            return new ReadTaskDto();
        }
        var taskDto = new ReadTaskDto()
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            TasksStatus = task.TaskStatus,
            DurationInMinutes = task.DurationInMinutes,
            TodoId = task.TodoId
        };
        return taskDto;
    }
    public async Task<ReadTaskDto> CreateTaskAsync(CreateTaskDto newTask)
    {
        if (newTask == null)
        {
            throw new ArgumentNullException(nameof(newTask));
        }
        var task = new Tasks
        {
            Name = newTask.Name,
            Description = newTask.Description,
            TaskStatus = newTask.taskStatus,
            DurationInMinutes = newTask.DurationInMinutes,
            TodoId = newTask.TodoId
        };
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        var taskDto = new ReadTaskDto
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            TasksStatus = task.TaskStatus,
            DurationInMinutes = task.DurationInMinutes,
            TodoId = task.TodoId
        };
        return taskDto;
    }


    public Task<ReadTaskDto> UpdateTaskAsync(int id, UpdateTaskDto task)
    {
        throw new NotImplementedException();
    }
    public Task<bool> DeleteTaskAsync(int id)
    {
        throw new NotImplementedException();
    }
}
