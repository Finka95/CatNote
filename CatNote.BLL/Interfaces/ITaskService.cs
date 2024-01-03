using CatNote.BLL.Models;

namespace CatNote.BLL.Interfaces;

public interface ITaskService : IGenericService<TaskModel>
{
    Task<List<TaskModel>> GetTasksByUserId(int userId, CancellationToken cancellationToken);
}
