using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatNote.DAL.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(ApplicationDbContext applicationDbContext)
    : base(applicationDbContext)
    {
    }

    public async Task<UserEntity?> GetUserByIdWithTasksAchievements(int userId, CancellationToken cancellationToken)
    {
        var user = await dbSet.Include(x => x.Tasks).Include(x => x.Achievements).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        return user;
    }

    public async Task<List<UserEntity>> GetUsersWithAchievements(CancellationToken cancellationToken)
    {
        return await dbSet.Include(x => x.Achievements).ToListAsync(cancellationToken);
    }

    public async Task<UserEntity?> GetUserByUserName(string userName, CancellationToken cancellationToken)
    {
        return await dbSet.FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);
    }
}
