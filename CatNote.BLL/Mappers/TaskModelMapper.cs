using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.BLL.Mappers;

public static class TaskModelMapper
{
    public static TaskEntity ToEntity(TaskModel taskModel) => new TaskEntity
    {
        Id = taskModel.Id,
        Date = taskModel.Date,
        Status = taskModel.Status,
        Title = taskModel.Title
    };

    public static TaskModel FromEntity(TaskEntity taskEntity) => new TaskModel
    {
        Id = taskEntity.Id,
        Date = taskEntity.Date,
        Title = taskEntity.Title,
        Status = taskEntity.Status
    };
}