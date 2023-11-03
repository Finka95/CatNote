namespace CatNote.DAL.Entities
{
    public class AchievementEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<UserEntity>? Users { get; set; }
    }
}
