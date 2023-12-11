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

    [HttpGet("achievements/points")]
    public async Task<List<UserDTO>> GetUserByPoints(CancellationToken cancellationToken)
    {
        var users = await _service.GetUsersByAchievementPoints(cancellationToken);

        return _mapper.Map<List<UserDTO>>(users);
    }

    [HttpGet("check")]
    public async Task<bool> CheckUserByUserName(UserDTO userDTO, CancellationToken cancellationToken)
    {
        var user = await _service.GetUserByUserName(userDTO.UserName!, cancellationToken);

        return user == null;
    }

    [HttpGet("id")]
    public async Task<UserDTO> GetUserByUserName(UserDTO userDTO, CancellationToken cancellationToken)
    {
        var user = await _service.GetUserByUserName(userDTO.UserName!, cancellationToken);

        return _mapper.Map<UserDTO>(user);
    }
}
