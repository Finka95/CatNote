using CatNote.DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace CatNote.DAL;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<AchievementEntity> Achievements { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<AchievementUserEntity> AchievementsUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AchievementUserEntity>(b =>
        {
            b.HasKey(x => new
            {
                x.AchievementId,
                x.UserId
            });

            b.HasOne(x => x.Achievement)
                .WithMany()
                .HasForeignKey(x => x.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
