namespace CatNote.DAL.Entities;

public class TaskEntity : BaseEntity
{
    public string? Title { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime Date { get; set; }
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;
}
