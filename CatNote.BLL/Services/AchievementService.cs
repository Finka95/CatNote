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

    public async Task CheckAchievementToAdd(int userId, CancellationToken cancellationToken)
    {
        var userModel = await GetUserModel(userId, cancellationToken);

        if (userModel == null)
            return;

        var achievement = await GetAchievementModel(userModel.Tasks!.Count(), AchievementType.ToAdd, cancellationToken);

        if (achievement == null || userModel.Achievements.Contains(achievement))
            return;

        await AddAchievementToUser(achievement.AchievementId, userId, cancellationToken);
    }

    public async Task CheckAchievementToComplete(int userId, CancellationToken cancellationToken)
    {
        var userModel = await GetUserModel(userId, cancellationToken);

        if (userModel == null)
            return;
        
        var completedTaskCount = userModel!.Tasks!.Count(x => x.Status == TaskStatus.Done);

        var achievement =
            await GetAchievementModel(completedTaskCount, AchievementType.CompletedTask, cancellationToken);

        if (achievement == null || userModel.Achievements.Contains(achievement))
            return;

        await AddAchievementToUser(achievement.AchievementId, userId, cancellationToken);
    }

    private async Task<UserModel?> GetUserModel(int userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdWithTasksAchievements(userId, cancellationToken);
        var userModel = _mapper.Map<UserModel>(user);

        if (userModel?.Tasks == null) //нужно ли тут
            return null;

        return userModel;
    }

    private async Task<Achievement?> GetAchievementModel(int taskCount, AchievementType achievementType,
        CancellationToken cancellationToken)
    {
        var achievementsEntity =
            await _achievementRepository.GetAchievementByTaskCountAchievementType(taskCount, achievementType, cancellationToken);

        var achievement = _mapper.Map<Achievement>(achievementsEntity);

        return achievement;
    }

    private async Task AddAchievementToUser(int achievementId, int userId,
        CancellationToken cancellationToken)
    {
        await _achievementRepository.AddConnectionBetweenUserAndAchievement(achievementId, userId, cancellationToken);
    }
}
