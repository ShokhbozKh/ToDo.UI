using System.ComponentModel;
using ToDo.UI.Const;
using ToDo.UI.Models;

namespace ToDo.UI.DTOs.TaskDto;

public class ReadTaskDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TasksStatus TasksStatus { get; set; }
    public int DurationInMinutes { get; set; }
    [DisplayName("Todo Name")]
    public int TodoId { get; set; }

}
