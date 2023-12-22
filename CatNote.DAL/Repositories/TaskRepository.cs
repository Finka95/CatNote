using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatNote.DAL.Repositories;

public class TaskRepository :GenericRepository<TaskEntity>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext applicationDbContext)
    : base(applicationDbContext)
    {
    }

    public async Task<List<TaskEntity>> GetTasksByUserId(int userId, CancellationToken cancellationToken)
    {
        var tasks = await dbSet.Where(x => x.UserId == userId).ToListAsync(cancellationToken);

        return tasks;
    }
}
