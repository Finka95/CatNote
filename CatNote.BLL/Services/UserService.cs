using AutoMapper;
using CatNote.BLL.AchievementTypes;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using CatNote.DAL.Repositories;

namespace CatNote.BLL.Services;

public class UserService : GenericService<UserModel, UserEntity>, IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(
        IMapper mapper,
        IUserRepository userRepository)
        :base(mapper, userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<List<Achievement>> GetAchievementsByUserId(int userId, CancellationToken cancellationToken)
    {
        var achievementsEntity = await _userRepository.GetAchievementsByUserId(userId, cancellationToken);

        var achievements = _mapper.Map<List<Achievement>>(achievementsEntity);

        return achievements;
    }
}
