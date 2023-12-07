using CatNote.Domain.Enums;

namespace CatNote.API.DTO;

public class TaskDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public Domain.Enums.TaskStatus Status { get; set; }
    public DateTime Date { get; set; }
    public int UserId { get; set; }
}
