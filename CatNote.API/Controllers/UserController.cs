using AutoMapper;
using CatNote.API.DTO;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatNote.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : GenericController<UserModel, UserDTO>
{
    private readonly IMapper _mapper;
    private readonly IUserService _service;

    public UserController(IMapper mapper, IUserService service)
        : base(mapper, service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpGet("{id}/achievements")]
    public async Task<List<AchievementDTO>> GetAchievementsByUserId(int id, CancellationToken cancellationToken)
    {
        var resultModel = await _service.GetAchievementsByUserId(id, cancellationToken);

        return _mapper.Map<List<AchievementDTO>>(resultModel);
    }
}
