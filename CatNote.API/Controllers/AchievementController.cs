using AutoMapper;
using CatNote.API.DTO;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;

namespace CatNote.API.Controllers;

public class AchievementController : GenericController<AchievementModel, AchievementDTO>
{
    public AchievementController(IMapper mapper, IGenericService<AchievementModel> service)
        : base(mapper, service)
    {
    }
}