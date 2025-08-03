using ToDo.UI.Const;

namespace ToDo.UI.Models;

public class Todos
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TodoStatus ToDoStatus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public double Progress { get; set; } = 0.0;
    public IEnumerable<Tasks> Tasks { get; set; } = new List<Tasks>();
}
