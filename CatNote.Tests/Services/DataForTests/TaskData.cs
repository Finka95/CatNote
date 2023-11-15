using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.Tests.Services.DataForTests;

public static class TaskData
{
    public static TaskModel TaskModel =
        new() { Id = 1, Title = "defaultTitle", Date = DateTime.Today, Status = TaskStatus.Canceled };

    public static TaskEntity TaskEntity =
        new() { Id = 1, Title = "defaultTitle", Date = DateTime.Today, Status = TaskStatus.Canceled };
}
