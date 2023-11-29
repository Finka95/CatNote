using CatNote.API.DTO;
using TaskStatus = CatNote.Domain.Enums.TaskStatus;

namespace CatNote.IntegrationTests.DataForIntegrationTests;
internal static class TaskData
{
    internal static TaskDTO TaskDTO(int id) =>
        new() { Id = id, Title = "defaultTitle", Date = DateTime.Today, Status = TaskStatus.ToDo };
}
