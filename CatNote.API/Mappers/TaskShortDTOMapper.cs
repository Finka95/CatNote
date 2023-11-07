using CatNote.API.DTO;
using CatNote.BLL.Models;
using CatNote.Common.Interfaces;

namespace CatNote.API.Mappers;

public class TaskShortDTOMapper : IMapper<TaskModel, TaskShortDTO>
{
    public TaskModel ToEntity(TaskShortDTO taskShortDTO) => new TaskModel
    {
        Id = taskShortDTO.Id,
        Title = taskShortDTO.Title
    };

    public TaskShortDTO FromEntity(TaskModel taskModel) => new TaskShortDTO
    {
        Id = taskModel.Id,
        Title = taskModel.Title
    };
}