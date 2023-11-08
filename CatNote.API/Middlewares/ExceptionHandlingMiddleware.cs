﻿using CatNote.BLL.Exceptions;

namespace CatNote.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            await context.Response.WriteAsync(ex.Message);
        }
        catch (BadRequestException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsync("Internal Server Error. Please contact the administrator.");
        }
    }
}