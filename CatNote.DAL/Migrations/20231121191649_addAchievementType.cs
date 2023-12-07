using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatNote.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addAchievementType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AchievementTypeNum",
                table: "Achievements",
                newName: "AchievementType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AchievementType",
                table: "Achievements",
                newName: "AchievementTypeNum");
        }
    }
}
