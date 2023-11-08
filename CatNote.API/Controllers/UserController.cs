using CatNote.API.DTO;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Mappers.Abstractions;
using CatNote.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatNote.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : GenericController<UserModel, UserDTO>
{
    public UserController(IMapper<UserModel, UserDTO> mapper, IGenericService<UserModel> service)
        : base(mapper, service)
    {
    }
}