using ToDo.UI.Const;
using ToDo.UI.Models;

namespace ToDo.UI.DTOs.TaskDto;
public class UpdateTaskDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public TasksStatus TasksStatus { get; set; }
    public int DurationInMinutes { get; set; }
    public int TodoId { get; set; }

}
