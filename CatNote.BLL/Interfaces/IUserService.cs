using CatNote.BLL.Models;

namespace CatNote.BLL.Interfaces;

public interface IUserService : IGenericService<UserModel>
{
    Task<List<UserModel>> GetUsersByAchievementPoints(CancellationToken cancellationToken);
    Task<UserModel?> GetUserByUserName(string userName, CancellationToken cancellationToken);
}
