using CatNote.API.DTO;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Mappers.Abstractions;
using CatNote.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatNote.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : GenericController<TaskModel, TaskDTO>
{
    public TaskController(IMapper<TaskModel, TaskDTO> mapper, IGenericService<TaskModel> service)
        : base(mapper, service)
    {
    }
}