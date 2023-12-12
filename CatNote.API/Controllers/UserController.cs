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
    private readonly IUserService _service;

    public UserController(IMapper mapper, IUserService service)
        : base(mapper, service)
    {
        _service = service;
    }

    [HttpGet("achievements/points")]
    public async Task<List<UserDTO>> GetUserByPoints(CancellationToken cancellationToken)
    {
        var users = await _service.GetUsersByAchievementPoints(cancellationToken);

        return _mapper.Map<List<UserDTO>>(users);
    }

    [HttpPost("{userName}")]
    public async Task<UserDTO> GetOrCreateUser(string userName, CancellationToken cancellationToken)
    {
        var userCheck = await _service.GetUserByUserName(userName, cancellationToken);
        var userCheckDTO = _mapper.Map<UserDTO>(userCheck);

        if (userCheckDTO == null)
        {
            var userModel = new UserModel { UserName = userName };
            userCheck = await _service.Create(userModel, cancellationToken);
            userCheckDTO = _mapper.Map<UserDTO>(userCheck);
        }
        return userCheckDTO;
    }
}
