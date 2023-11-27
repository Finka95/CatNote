using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
