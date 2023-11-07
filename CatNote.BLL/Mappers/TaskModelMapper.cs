using CatNote.BLL.Mappers.Abstractions;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.BLL.Mappers;

public class TaskModelMapper : IMapper<TaskEntity, TaskModel>
{
    public TaskEntity ToEntity(TaskModel taskModel) => new ()
    {
        Id = taskModel.Id,
        Date = taskModel.Date,
        Status = taskModel.Status,
        Title = taskModel.Title
    };

    public TaskModel FromEntity(TaskEntity taskEntity) => new ()
    {
        Id = taskEntity.Id,
        Date = taskEntity.Date,
        Title = taskEntity.Title,
        Status = taskEntity.Status
    };
}