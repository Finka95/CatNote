﻿using System.Security.Claims;
using CatNote.API.Configurations;
using CatNote.API.Mappers;
using CatNote.API.Middlewares;
using CatNote.BLL.DI;
using CatNote.BLL.Mappers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CatNote.API;

public class Program
{
    private static string[] openIdArray = { "openid" };

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddFluentValidationAutoValidation();

        var connection = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddBusinessServices(connection);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        var authConfig = builder.Configuration.GetSection("Auth0").Get<AuthenticationConfiguration>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
                options.Audience = builder.Configuration["Auth0:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier,

                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = authConfig!.GetSecurityKey()
                };
            });

        builder.Services.AddAutoMapper(typeof(MapperApiProfile).Assembly, typeof(MapperBllProfile).Assembly);

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Documentation",
                Version = "v1.0",
                Description = ""
            });
            options.ResolveConflictingActions(x => x.First());
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                BearerFormat = "JWT",
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"https://{builder.Configuration["Auth0:Domain"]}/authorize?audience={builder.Configuration["Auth0:Audience"]}"),
                        TokenUrl = new Uri($"https://{builder.Configuration["Auth0:Domain"]}/oauth/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenId" },

                        }
                    }
                }
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    openIdArray
                }
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");
                c.OAuthClientId(builder.Configuration["Auth0:ClientId"]);
                c.OAuthClientSecret(builder.Configuration["Auth0:ClientSecret"]);
                c.OAuthUsePkce();
            });
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
