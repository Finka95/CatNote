using AutoMapper;
using CatNote.API.DTO;
using CatNote.BLL.AchievementTypes;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;

namespace CatNote.API.Controllers;

public class AchievementController : GenericController<Achievement, AchievementDTO>
{
    public AchievementController(IMapper mapper, IGenericService<Achievement> service)
        : base(mapper, service)
    {
    }
}
