using ToDo.UI.Const;

namespace ToDo.UI.Models;

public class Tasks
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TodoStatus ToDoStatus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int TodoId { get; set; }
    public Todos Todo { get; set; }

}
