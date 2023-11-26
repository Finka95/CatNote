using CatNote.BLL.AchievementTypes;
using CatNote.BLL.Models;

namespace CatNote.BLL.Interfaces;

public interface IUserService : IGenericService<UserModel>
{
    public Task<List<Achievement>> GetAchievementsByUserId(int userId, CancellationToken cancellationToken);
}
