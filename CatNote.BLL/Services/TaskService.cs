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
    private readonly ITaskRepository _taskRepository;

    public TaskService(IMapper mapper, ITaskRepository taskRepository, IAchievementService achievementService)
        :base(mapper, taskRepository)
    {
        _mapper = mapper;
        _taskRepository = taskRepository;
        _achievementService = achievementService;
    }

    public override async Task<TaskModel> Create(TaskModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TaskEntity>(model);

        var resultEntity = await _taskRepository.Create(entity, cancellationToken);

        await _achievementService.CheckAchievementToAdd(model.UserId, cancellationToken);

        return _mapper.Map<TaskModel>(resultEntity);
    }

    public override async Task<TaskModel> Update(TaskModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TaskEntity>(model);

        var resultEntity = await _taskRepository.Update(entity, cancellationToken);

        await _achievementService.CheckAchievementToComplete(model.UserId, cancellationToken);

        return _mapper.Map<TaskModel>(resultEntity);
    }
}
