using Microsoft.EntityFrameworkCore;
using ToDo.UI.Models;
using ToDo.UI.DTOs.TaskDto;
using ToDo.UI.DTOs.TodoDto;

namespace ToDo.UI.Data;
public class TodoDbContext:DbContext
{
    public virtual DbSet<Todos> Todos { get; set; }
    public virtual DbSet<Tasks> Tasks { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options) 
        : base(options)
    {
    }
    public DbSet<ToDo.UI.DTOs.TodoDto.ReadToDoDto> ReadToDoDto { get; set; } = default!;
    public DbSet<ToDo.UI.DTOs.TodoDto.CreateToDoDto> CreateToDoDto { get; set; } = default!;
}
