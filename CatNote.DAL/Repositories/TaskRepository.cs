using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;

namespace CatNote.DAL.Repositories;

public class TaskRepository :GenericRepository<TaskEntity>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext applicationDbContext)
    : base(applicationDbContext)
    {
    }
}
