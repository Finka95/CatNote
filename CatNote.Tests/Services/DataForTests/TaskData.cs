using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using TaskStatus = CatNote.Domain.Enums.TaskStatus;

namespace CatNote.Tests.Services.DataForTests;

internal static class TaskData
{

    internal static TaskModel TaskModel =
        new() { Id = 1, Title = "defaultTitle", Date = DateTime.Today, Status = TaskStatus.Canceled };

    internal static TaskEntity TaskEntity =
        new() { Id = 1, Title = "defaultTitle", Date = DateTime.Today, Status = TaskStatus.Canceled };
}
