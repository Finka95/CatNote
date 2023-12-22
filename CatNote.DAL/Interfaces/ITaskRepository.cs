using CatNote.DAL.Entities;

namespace CatNote.DAL.Interfaces;

public interface ITaskRepository : IGenericRepository<TaskEntity>
{
    Task<List<TaskEntity>> GetTasksByUserId(int userId, CancellationToken cancellationToken);
}
