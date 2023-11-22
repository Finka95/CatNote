using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.DAL.Entities;

namespace CatNote.DAL.Interfaces;

public interface IUserRepository
{
    Task<UserEntity?> GetUserByIdWithTasksAchievements(int userId, CancellationToken  cancellationToken);
}
