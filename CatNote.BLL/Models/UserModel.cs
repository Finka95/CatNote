using Microsoft.AspNetCore.Identity;

namespace CatNote.BLL.Models
{
    public class UserModel : IdentityUser
    {
        public IEnumerable<Task>? Tasks { get; set; }
        public IEnumerable<AchievementModel>? Achievements { get; set;}
    }
}
