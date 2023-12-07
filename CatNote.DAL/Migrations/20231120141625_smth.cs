using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatNote.DAL.Migrations
{
    /// <inheritdoc />
    public partial class smth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AchievementType",
                table: "Achievements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AchievementType",
                table: "Achievements");
        }
    }
}
