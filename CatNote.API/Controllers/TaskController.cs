using AutoMapper;
using CatNote.API.DTO;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatNote.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : GenericController<TaskModel, TaskDTO>
{
    private readonly ITaskService _service;

    public TaskController(IMapper mapper, ITaskService service)
        : base(mapper, service)
    {
        _service = service;
    }


    [HttpGet("user/{userId}")]
    public async Task<List<TaskDTO>> GetTasksByUserId(int userId, CancellationToken cancellationToken)
    {
        var resultModel = await _service.GetTasksByUserId(userId, cancellationToken);

        return _mapper.Map<List<TaskDTO>>(resultModel);
    }
}
