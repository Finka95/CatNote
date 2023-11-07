using CatNote.API.DTO;
using CatNote.BLL.Models;
using CatNote.Domain.Interfaces;

namespace CatNote.API.Mappers;

public class TaskDTOMapper : IMapper<TaskModel, TaskDTO>
{
    public TaskModel ToEntity(TaskDTO taskDTO) => new ()
    {
        Id = taskDTO.Id,
        Title = taskDTO.Title,
        Date = taskDTO.Date,
        Status = taskDTO.Status
    };

    public TaskDTO FromEntity(TaskModel taskModel) => new ()
    {
        Id = taskModel.Id,
        Title = taskModel.Title,
        Date = taskModel.Date,
        Status = taskModel.Status
    };
}