using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatNote.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<UserEntity?> GetUserByIdWithTasksAchievements(int userId, CancellationToken cancellationToken)
    {
        var user = await _applicationDbContext.Users.Include(x => x.Tasks).Include(x => x.Achievements).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        return user;
    }
}
