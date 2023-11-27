using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.Models;

namespace CatNote.BLL.Interfaces;

public interface IAchievementService : IGenericService<AchievementModel>
{
    Task CheckAchievementToAdd(int userId, CancellationToken cancellationToken);
    Task CheckAchievementToComplete(int userId, CancellationToken cancellationToken);
    Task<List<AchievementModel>> GetAchievementsByUserId(int userId, CancellationToken cancellationToken);
}
