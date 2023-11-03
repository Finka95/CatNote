using Microsoft.AspNetCore.Identity;

namespace CatNote.DAL.Entities
{
    public class UserEntity : IdentityUser
    {
        public IEnumerable<Task>? Tasks { get; set; }
        public IEnumerable<AchievementEntity>? Achievements { get; set;}
    }
}
