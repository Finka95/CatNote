using CatNote.API.Enums;

namespace CatNote.API.DTO;

public class TaskDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public TaskStatusDTO Status { get; set; }
    public DateTime Date { get; set; }
}
