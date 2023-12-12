using AutoMapper;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using CatNote.DAL.Repositories;

namespace CatNote.BLL.Services;

public class UserService : GenericService<UserModel, UserEntity>, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IMapper mapper,
        IUserRepository userRepository)
        :base(mapper, userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserModel>> GetUsersByAchievementPoints(CancellationToken cancellationToken)
    {
        var usersEntity = await _userRepository.GetUsersWithAchievements(cancellationToken);

        var users = _mapper.Map<List<UserModel>>(usersEntity);

        return users.OrderByDescending(x => x.Achievements?.Select(y => y.Point).Sum()).ToList();
    }

    public async Task<UserModel?> GetUserByUserName(string userName, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetUserByUserName(userName, cancellationToken);

        if (userEntity == null)
            return null;

        return _mapper.Map<UserModel>(userEntity);
    }
}
