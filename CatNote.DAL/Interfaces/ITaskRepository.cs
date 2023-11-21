using CatNote.DAL.Entities;

namespace CatNote.DAL.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskEntity>> GetTasksByUserId(int userId, CancellationToken cancellationToken);
}
