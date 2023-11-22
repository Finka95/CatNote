using AutoMapper;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;

namespace CatNote.BLL.Services;

public class TaskService : GenericService<TaskModel, TaskEntity>, ITaskService
{
    private readonly IAchievementService _achievementService;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<TaskEntity> _genericRepository;

    public TaskService(IMapper mapper, IGenericRepository<TaskEntity> genericRepository, IAchievementService achievementService)
        :base(mapper, genericRepository)
    {
        _mapper = mapper;
        _genericRepository = genericRepository;
        _achievementService = achievementService;
    }

    public override async Task<TaskModel> Create(TaskModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TaskEntity>(model);

        var resultEntity = await _genericRepository.Create(entity, cancellationToken);

        await _achievementService.CheckAchievement(model.UserId, cancellationToken);

        return _mapper.Map<TaskModel>(resultEntity);
    }

    public override async Task<TaskModel> Update(TaskModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TaskEntity>(model);

        var resultEntity = await _genericRepository.Update(entity, cancellationToken);

        await _achievementService.CheckAchievement(model.UserId, cancellationToken);

        return _mapper.Map<TaskModel>(resultEntity);
    }
}
