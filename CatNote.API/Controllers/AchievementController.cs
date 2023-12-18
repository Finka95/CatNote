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
public class AchievementController : GenericController<AchievementModel, AchievementDTO>
{
    private readonly IAchievementService _service;

    public AchievementController(IMapper mapper, IAchievementService service)
        : base(mapper, service)
    {
        _service = service;
    }

    [HttpGet("user/{userId}")]
    public async Task<List<AchievementDTO>> GetAchievementsByUserId(int userId, CancellationToken cancellationToken)
    {
        var resultModel = await _service.GetAchievementsByUserId(userId, cancellationToken);

        return _mapper.Map<List<AchievementDTO>>(resultModel);
    }
}
