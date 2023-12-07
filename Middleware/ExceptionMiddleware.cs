using System.Net;
using System.Text.Json;
using e_commerce_app.Errors;

namespace e_commerce_app.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            //If there is no exception, then the request will move on to the next stage
            await _next(context);
        }
        catch (Exception ex)
        {
            //Log the exception (Console)
            _logger.LogError(ex, ex.Message);
            //Write the exception to the response and send it to the client
            //All are sent as JSON
            context.Response.ContentType = "application/json";
            //Set the status code to 500
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            //Write our own response
            //If we are in development, we will return the exception message and stack trace
            //If we are in production, we will return a generic message
            //Using ternary operator
            var response = _env.IsDevelopment()
                ? new ApiException((int) HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                //If we are not in development, we will return a generic message
                : new ApiException((int) HttpStatusCode.InternalServerError);

            //We need to convert the response to JSON
            var options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

            //Serialize the response
            var json = JsonSerializer.Serialize(response, options);

            //Write the response to the client
            await context.Response.WriteAsync(json);
            
            //Add it as a middleware in Program.cs
        }
    }

}