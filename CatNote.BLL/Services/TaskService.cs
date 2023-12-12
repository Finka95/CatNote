using AutoMapper;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using CatNote.Domain.Exceptions;

namespace CatNote.BLL.Services;

public class TaskService : GenericService<TaskModel, TaskEntity>, ITaskService
{
    private readonly IAchievementService _achievementService;

    public TaskService(
        IMapper mapper,
        ITaskRepository taskRepository,
        IAchievementService achievementService)
        :base(mapper, taskRepository)
    {
        _achievementService = achievementService;
    }

    public override async Task<TaskModel> Create(TaskModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TaskEntity>(model);

        var resultEntity = await _genericRepository.Create(entity, cancellationToken);

        await _achievementService.CheckAchievementToAdd(model.UserId, cancellationToken);

        return _mapper.Map<TaskModel>(resultEntity);
    }

    public override async Task<TaskModel> Update(TaskModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TaskEntity>(model);

        var findTask = await _genericRepository.GetById(model.Id, cancellationToken);

        if (findTask != null)
        {
            var resultEntity = await _genericRepository.Update(entity, cancellationToken);

            await _achievementService.CheckAchievementToComplete(model.UserId, cancellationToken);

            return _mapper.Map<TaskModel>(resultEntity);
        }
        else
        {
            throw new NotFoundException("Entity with Id {element.Id} not found.");
        }
    }
}
