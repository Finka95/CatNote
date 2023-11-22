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
    public UserController(IMapper mapper, IGenericService<UserModel> service)
        : base(mapper, service)
    {

    }

    //[HttpGet("{id}")]
    //public async Task<> GetById(int id, CancellationToken cancellationToken)
    //{
    //    var resultModel = await _service.GetById(id, cancellationToken);

    //    return _mapper.Map<TDTO>(resultModel);
    //}
}
