using CatNote.API.DTO;
using CatNote.BLL.Models;
using CatNote.Common.Interfaces;

namespace CatNote.API.Mappers;

public class TaskDTOMapper : IMapper<TaskModel, TaskDTO>
{
    public TaskModel ToEntity(TaskDTO taskDTO) => new TaskModel
    {
        Id = taskDTO.Id,
        Title = taskDTO.Title,
        Date = taskDTO.Date,
        Status = taskDTO.Status
    };

    public TaskDTO FromEntity(TaskModel taskModel) => new TaskDTO
    {
        Id = taskModel.Id,
        Title = taskModel.Title,
        Date = taskModel.Date,
        Status = taskModel.Status
    };
}