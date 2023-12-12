using AutoMapper;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using CatNote.Domain.Enums;
using CatNote.Domain.Exceptions;
using TaskStatus = CatNote.Domain.Enums.TaskStatus;

namespace CatNote.BLL.Services;

public class AchievementService : GenericService<AchievementModel, AchievementEntity>, IAchievementService
{
    private readonly IAchievementRepository _achievementRepository;
    private readonly IUserRepository _userRepository;

    public AchievementService(
        IMapper mapper, 
        IAchievementRepository achievementRepository,
        IUserRepository userRepository)
        :base(mapper, achievementRepository)
    {
        _achievementRepository = achievementRepository;
        _userRepository = userRepository;
    }

    public async Task<List<AchievementModel>> GetAchievementsByUserId(int userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(userId, cancellationToken);

        if (user == null)
        {
            return new List<AchievementModel>();
        }

        var achievementsEntity = await _achievementRepository.GetAchievementsByUser(user, cancellationToken);

        var achievements = _mapper.Map<List<AchievementModel>>(achievementsEntity);

        return achievements;
    }

    public async Task CheckAchievementToAdd(int userId, CancellationToken cancellationToken)
    {
        var user = await GetUserModelById(userId, cancellationToken);

        if (user == null)
            throw new NotFoundException($"User with id {userId} not found");

        if (user.Tasks == null)
            return;

        var achievement = await GetAchievementByParameters(user.Tasks.Count(), AchievementType.Add, cancellationToken);

        if (achievement == null || user.Achievements!.Contains(achievement))
            return;

        await _achievementRepository.AddConnectionBetweenUserAndAchievement(achievement.Id, userId, cancellationToken);
    }

    public async Task CheckAchievementToComplete(int userId, CancellationToken cancellationToken)
    {
        var user = await GetUserModelById(userId, cancellationToken);

        if (user == null || user.Tasks == null)
            return;

        var completedTaskCount = user.Tasks.Count(x => x.Status == TaskStatus.Done);
        var achievement = await GetAchievementByParameters(completedTaskCount, AchievementType.Completed, cancellationToken);

        if (completedTaskCount == 0 || achievement == null || user.Achievements!.Contains(achievement))
            return;

        await _achievementRepository.AddConnectionBetweenUserAndAchievement(achievement.Id, userId, cancellationToken);
    }

    private async Task<UserModel?> GetUserModelById(int userId, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetUserByIdWithTasksAchievements(userId, cancellationToken);
        var user = _mapper.Map<UserModel>(userEntity);

        return user;
    }

    private async Task<AchievementModel?> GetAchievementByParameters(int taskCount, AchievementType achievementType,
        CancellationToken cancellationToken)
    {
        var achievementEntity =
            await _achievementRepository.GetAchievementByTaskCountAchievementType(taskCount, achievementType, cancellationToken);

        var achievement = _mapper.Map<AchievementModel>(achievementEntity);

        return achievement;
    }
}
