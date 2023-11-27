using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.Interfaces;
using AutoMapper;
using System.Diagnostics;
using CatNote.BLL.AchievementTypes;
using CatNote.DAL.Repositories;
using CatNote.Domain.Enums;
using TaskStatus = CatNote.Domain.Enums.TaskStatus;

namespace CatNote.BLL.Services;

public class AchievementService : GenericService<Achievement, AchievementEntity>, IAchievementService
{
    private readonly IMapper _mapper;
    private readonly IAchievementRepository _achievementRepository;
    private readonly IUserRepository _userRepository;

    public AchievementService(
        IMapper mapper, 
        IAchievementRepository achievementRepository,
        IUserRepository userRepository)
        :base(mapper, achievementRepository)
    {
        _mapper = mapper;
        _achievementRepository = achievementRepository;
        _userRepository = userRepository;
    }

    public async Task<List<Achievement>> GetAchievementsByUserId(int userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(userId, cancellationToken);

        if (user == null)
        {
            return new List<Achievement>();
        }

        var achievementsEntity = await _achievementRepository.GetAchievementsByUser(user, cancellationToken);

        var achievements = _mapper.Map<List<Achievement>>(achievementsEntity);

        return achievements;
    }

    public async Task CheckAchievementToAdd(int userId, CancellationToken cancellationToken)
    {
        var user = await GetUserModelById(userId, cancellationToken);

        var achievement = await GetAchievementByParameters(user!.Tasks!.Count(), AchievementType.Add, cancellationToken);

        if (user == null || achievement == null || user.Achievements!.Contains(achievement))
            return;

        await _achievementRepository.AddConnectionBetweenUserAndAchievement(achievement.Id, userId, cancellationToken);
    }

    public async Task CheckAchievementToComplete(int userId, CancellationToken cancellationToken)
    {
        var user = await GetUserModelById(userId, cancellationToken);
        var completedTaskCount = user!.Tasks!.Count(x => x.Status == TaskStatus.Done);
        var achievement = await GetAchievementByParameters(completedTaskCount, AchievementType.Completed, cancellationToken);

        if (user == null || completedTaskCount == 0 || achievement == null || user.Achievements!.Contains(achievement))
            return;

        await _achievementRepository.AddConnectionBetweenUserAndAchievement(achievement.Id, userId, cancellationToken);
    }

    private async Task<UserModel?> GetUserModelById(int userId, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetUserByIdWithTasksAchievements(userId, cancellationToken);
        var user = _mapper.Map<UserModel>(userEntity);

        return user;
    }

    private async Task<Achievement?> GetAchievementByParameters(int taskCount, AchievementType achievementType,
        CancellationToken cancellationToken)
    {
        var achievementEntity =
            await _achievementRepository.GetAchievementByTaskCountAchievementType(taskCount, achievementType, cancellationToken);

        var achievement = _mapper.Map<Achievement>(achievementEntity);

        return achievement;
    }
}
