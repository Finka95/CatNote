using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatNote.DAL.Entities;

public class AchievementUserEntity
{
    public int UserId { get; set; }
    public int AchievementId { get; set;}
    public UserEntity User { get; set; }
    public AchievementEntity Achievement { get; set; }
}
