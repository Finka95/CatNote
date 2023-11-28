using CatNote.API.Mappers;
using CatNote.API.Middlewares;
using CatNote.API.ScopeSettings;
using CatNote.BLL.DI;
using CatNote.BLL.Mappers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CatNote.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddFluentValidationAutoValidation();

        var connection = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddBusinessServices(connection);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
            options.Audience = builder.Configuration["Auth0:Audience"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier,
            };
        });

        builder.Services
          .AddAuthorization(options =>
          {
              options.AddPolicy(
                "read:messages",
                policy => policy.Requirements.Add(
                  new HasScopeRequirement("read:messages", builder.Configuration["Auth0:Domain"])
                )
              );
          });

        builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        builder.Services.AddAutoMapper(typeof(MapperApiProfile).Assembly, typeof(MapperBllProfile).Assembly);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
