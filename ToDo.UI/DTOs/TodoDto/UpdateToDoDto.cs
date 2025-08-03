using ToDo.UI.Const;
using ToDo.UI.Models;

namespace ToDo.UI.DTOs.TodoDto;

public class UpdateToDoDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TodoStatus ToDoStatus { get; set; }

}
