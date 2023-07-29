﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CarWorkshop.Application.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
	private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try 
		{ 
			await next.Invoke(context); 
		}

		catch (Exception ex)
		{
			_logger.LogError(ex, ex.Message);

			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			await context.Response.WriteAsync("Something went wrong");
		}
    }
}