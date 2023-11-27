﻿using Microsoft.EntityFrameworkCore.Migrations;

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
                @"IF (NOT EXISTS (SELECT 1 FROM [dbo].[Achievements] WHERE [Title] = 'First task'))
            BEGIN
            INSERT INTO [dbo].[Achievements] ([Title],[Description],[Point],[Type],[TaskCount])
            VALUES ('First task','Add first task', 1, 1, 1)
            END
            IF(NOT EXISTS (SELECT 1 FROM [dbo].[Achievements] WHERE [Title] = 'First three tasks'))
            BEGIN
            INSERT INTO [dbo].[Achievements] ([Title],[Description],[Point],[Type],[TaskCount])
            VALUES ('First three task','Add first three tasks', 3, 1, 3)
            END
            IF(NOT EXISTS (SELECT 1 FROM [dbo].[Achievements] WHERE [Title] = 'First five tasks'))
            BEGIN
            INSERT INTO [dbo].[Achievements] ([Title],[Description],[Point],[Type],[TaskCount])
            VALUES ('First five task','Add first five tasks', 5, 1, 5)
            END
            IF(NOT EXISTS (SELECT 1 FROM [dbo].[Achievements] WHERE [Title] = 'Completed first task'))
            BEGIN
            INSERT INTO [dbo].[Achievements] ([Title],[Description],[Point],[Type],[TaskCount])
            VALUES ('Completed first task','Completed first task', 2, 2, 1)
            END
            IF(NOT EXISTS (SELECT 1 FROM [dbo].[Achievements] WHERE [Title] = 'Completed first three tasks'))
            BEGIN
            INSERT INTO [dbo].[Achievements] ([Title],[Description],[Point],[Type],[TaskCount])
            VALUES ('Completed first three tasks','Completed first three tasks', 4, 2, 3)
            END
            ".Replace("'", "''");

            migrationBuilder.Sql($"EXECUTE('{script}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var script = @"IF (NOT EXISTS (SELECT 1 FROM [dbo].[Achievements] WHERE [Title] = 'First task'))
        BEGIN
            DELETE FROM [dbo].[Achievements]  WHERE [Title] = 'First task'
        END
        IF (NOT EXISTS (SELECT 1 FROM [dbo].[Achievements] WHERE [Title] = 'First three tasks'))
        BEGIN
            DELETE FROM [dbo].[Achievements]  WHERE [Title] = 'First three tasks'
        END
        IF (NOT EXISTS (SELECT 1 FROM [dbo].[Achievements] WHERE [Title] = 'First five tasks'))
        BEGIN
            DELETE FROM [dbo].[Achievements]  WHERE [Title] = 'First five tasks'
        END
        IF (NOT EXISTS (SELECT 1 FROM [dbo].[Achievements] WHERE [Title] = 'Completed first task'))
        BEGIN
            DELETE FROM [dbo].[Achievements]  WHERE [Title] = 'Completed first task'
        END
        IF (NOT EXISTS (SELECT 1 FROM [dbo].[Achievements] WHERE [Title] = 'Completed first three tasks'))
        BEGIN
            DELETE FROM [dbo].[Achievements]  WHERE [Title] = 'Completed first three tasks'
        END".Replace("'", "''");

            migrationBuilder.Sql($"EXECUTE('{script}')");
        }
    }
}
