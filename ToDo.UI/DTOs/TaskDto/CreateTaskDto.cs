using ToDo.UI.Const;
using ToDo.UI.Models;

namespace ToDo.UI.DTOs.TaskDto;

public class CreateTaskDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public TasksStatus taskStatus { get; set; } = TasksStatus.NotStarted;
    public int DurationInMinutes { get; set; }
    public int TodoId { get; set; }
}
