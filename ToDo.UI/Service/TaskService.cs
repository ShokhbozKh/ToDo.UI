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
            TodoId = t.TodoId,
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
            TodoId = task.TodoId,
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
            TaskStatus = newTask.TasksStatus,
            DurationInMinutes = newTask.DurationInMinutes,
            TodoId = (int)newTask.TodoId,
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
    public async Task<ReadTaskDto> UpdateTaskAsync(int id, UpdateTaskDto task)
    {
        var existingTask = await _context.Tasks
            .Include(t=>t.Todo)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (existingTask == null)
        {
            return null;
        }

        existingTask.Name = task.Name?? existingTask.Name;
        existingTask.Description = task.Description ?? existingTask.Description;
        existingTask.TaskStatus = task.TasksStatus != default ? task.TasksStatus : existingTask.TaskStatus;
        existingTask.DurationInMinutes = task.DurationInMinutes !=default ? task.DurationInMinutes:existingTask.DurationInMinutes;
        existingTask.TodoId = task.TodoId != default ? task.TodoId: existingTask.TodoId;
        _context.Update(existingTask);
        await _context.SaveChangesAsync();

        var updatedTaskDto = new ReadTaskDto
        {
            Id = existingTask.Id,
            Name = existingTask.Name,
            Description = existingTask.Description,
            TasksStatus = existingTask.TaskStatus,
            DurationInMinutes = existingTask.DurationInMinutes,
            TodoId = existingTask.TodoId
        };
        return updatedTaskDto;
    }
    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id);
        if (task == null)
        {
            return false;
        }
        _context.Tasks.Remove(task);    
        await _context.SaveChangesAsync();
        return true;
    }
}
