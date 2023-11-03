namespace CatNote.BLL.Models
{
    public class AchievementModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public IEnumerable<UserModel>? Users { get; set; }
    }
}
