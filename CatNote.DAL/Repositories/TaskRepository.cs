using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatNote.DAL.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public TaskRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<TaskEntity>> GetTasksByUserId(int userId, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Tasks.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
    }
}