using CatNote.API.DTO;
using CatNote.BLL.Models;
using CatNote.Domain.Interfaces;

namespace CatNote.API.Mappers;

public class TaskShortDTOMapper : IMapper<TaskModel, TaskShortDTO>
{
    public TaskModel ToEntity(TaskShortDTO taskShortDTO) => new ()
    {
        Id = taskShortDTO.Id,
        Title = taskShortDTO.Title
    };

    public TaskShortDTO FromEntity(TaskModel taskModel) => new ()
    {
        Id = taskModel.Id,
        Title = taskModel.Title
    };
}