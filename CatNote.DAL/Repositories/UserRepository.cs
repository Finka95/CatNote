using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatNote.DAL.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<UserEntity?> GetUserByIdWithTasksAchievements(int userId, CancellationToken cancellationToken)
    {
        var user = await _applicationDbContext.Users.Include(x => x.Tasks).Include(x => x.Achievements).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        return user;
    }

    public async Task<List<AchievementEntity>> GetAchievementsByUserId(int userId, CancellationToken cancellationToken)
    {
        var s = await dbContext.AchievementsUsers.ToListAsync(cancellationToken);

        var achievementsIds = s.Where(x => x.UserId == userId).Select(x => x.AchievementId).ToList();

        var achievements = await dbContext.Achievements.Where(x => achievementsIds.Contains(x.Id)).ToListAsync(cancellationToken);

        //var h = await dbContext.AchievementsUsers.ToListAsync(cancellationToken);
        //var a = await dbContext.Achievements.Include(x => x.Users).ToListAsync();
        //var j = await dbContext.Users.ToListAsync();
        //var r = await dbContext.Achievements.Include(x => x.Users).Where(x => x.Users!.Any(x => x.Id == userId)).ToListAsync(cancellationToken);

        return achievements;
    }
}
