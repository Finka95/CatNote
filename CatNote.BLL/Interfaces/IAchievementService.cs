using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.AchievementTypes;
using CatNote.BLL.Models;

namespace CatNote.BLL.Interfaces;

public interface IAchievementService : IGenericService<Achievement>
{
    public Task CheckAchievement(int userId, CancellationToken cancellationToken);
}