﻿using AutoMapper;
using CatNote.BLL.AchievementType;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using CatNote.DAL.Repositories;

namespace CatNote.BLL.Mappers;

public class MapperBllProfile : Profile
{
    private readonly ITaskRepository _taskRepository;

    public MapperBllProfile(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
        CreateMap<UserEntity, UserModel>().ReverseMap();
        CreateMap<TaskEntity, TaskModel>().ReverseMap();
        //CreateMap<AchievementEntity, Achievement>().ReverseMap();

        CreateMap<AchievementEntity, Achievement>()
            .ConstructUsing(src => ResolveAchievementType(src.AchievementTypeNum))
            .ReverseMap();
    }

    private Achievement ResolveAchievementType(int achievementTypeNum)
    {
        switch (achievementTypeNum)
        {
            case 0:
                return new AchievementToAddFirstTask(_taskRepository);

            default: return null;
        }
    }
}
