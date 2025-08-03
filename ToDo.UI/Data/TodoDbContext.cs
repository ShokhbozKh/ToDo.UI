using Microsoft.EntityFrameworkCore;
using ToDo.UI.Models;

namespace ToDo.UI.Data;
public class TodoDbContext:DbContext
{
    private readonly DbSet<Todos> todos;
    private readonly DbSet<Tasks> tasks;

    public TodoDbContext(DbContextOptions<TodoDbContext> options) 
        : base(options)
    {
    }
}
