using CatNote.API.Mappers;
using CatNote.API.Middlewares;
using CatNote.BLL.DI;
using CatNote.BLL.Mappers;
using FluentValidation.AspNetCore;

namespace CatNote.API;

public class Program
{
    private Program()
    {
        
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddFluentValidationAutoValidation();

        var connection = builder.Configuration.GetConnectionString("DefaultConnection");

        if (connection != null)
        {
            builder.Services.AddBusinessServices(connection);
        }

        builder.Services.AddAutoMapper(typeof(MapperApiProfile).Assembly, typeof(MapperBllProfile).Assembly);

        var app = builder.Build();

        //добавить авто миграции
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
