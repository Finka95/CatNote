using CatNote.API.DI;
using CatNote.BLL.DI;
using CatNote.DAL;
using CatNote.DAL.DI;
using CatNote.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatNote.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connection = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddBusinessServices(connection);
        builder.Services.AddMapperServices();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}