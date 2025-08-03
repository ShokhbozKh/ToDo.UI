using ToDo.UI.Const;
using ToDo.UI.Models;

namespace ToDo.UI.DTOs.TodoDto
{
    public class CreateToDoDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoStatus ToDoStatus { get; set; } = TodoStatus.NotStarted;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5);
    }
}
