using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.Tests.Services.DataForTests;

public static class TaskData
{
    public static TaskModel GetTaskModel() => new()
    {
        Id = 1,
        Title = "defaultTitle",
        Date = DateTime.Today,
        Status = TaskStatus.Canceled
    };

    public static TaskEntity GetTaskEntity() => new()
    {
        Id = 1,
        Title = "defaultTitle",
        Date = DateTime.Today,
        Status = TaskStatus.Canceled
    };
}
