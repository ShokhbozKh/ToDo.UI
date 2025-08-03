using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ToDo.UI.Const;
using ToDo.UI.Models;

namespace ToDo.UI.DTOs.TodoDto;

public class ReadToDoDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [DisplayName("Status")]
    public TodoStatus ToDoStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public double Progress { get; set; } // logika
}
