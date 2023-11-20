using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatNote.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var script =
                @"IF (NOT EXISTS (SELECT 1 FROM [dbo].[Achievements] WHERE [Title] = 'New user'))
            BEGIN
            INSERT INTO [dbo].[Achievements] (
                [Title],
                [Description],
                [AchievementType])
            VALUES (
                'New user',
                'Add first task'
                0)
            END".Replace("'", "''");

            migrationBuilder.Sql($"EXECUTE('{script}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var script = @"IF (NOT EXISTS (SELECT 1 FROM [dbo].[Achievements] WHERE [Title] = 'New user'))
                BEGIN
                    DELETE FROM [dbo].[Achievements]  WHERE [Title] = 'New user'
                END".Replace("'", "''");

            migrationBuilder.Sql($"EXECUTE('{script}')");
        }
    }
}
