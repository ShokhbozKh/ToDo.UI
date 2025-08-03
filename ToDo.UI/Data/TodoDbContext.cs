using Microsoft.EntityFrameworkCore;
using ToDo.UI.Models;

namespace ToDo.UI.Data;
public class TodoDbContext:DbContext
{
    public virtual DbSet<Todos> Todos { get; set; }
    public virtual DbSet<Tasks> Tasks { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options) 
        : base(options)
    {
    }
}
