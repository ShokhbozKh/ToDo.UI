using ToDo.UI.Const;

namespace ToDo.UI.Models;

public class Todos
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TodoStatus ToDoStatus { get; set; }
    public DateTime CreatedAt { get; set; } 
    public double Progress { get; set; }
    public IEnumerable<Tasks> Tasks { get; set; } = new List<Tasks>();
}
