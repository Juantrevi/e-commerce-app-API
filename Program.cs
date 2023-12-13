//Creates an instance of the web application using the default settings and runs it.

using e_commerce_app.Extensions;
using e_commerce_app.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//Middleware to redirect users to ErrorController when an error occurs
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

/*
 *  Swagger
    is a tool that can help you generate documentation for your API.
 *  https://localhost:5001/swagger
    Make it available for production and development environments
 */
app.UseSwagger();
app.UseSwaggerUI();

//This method is used to serve static files
app.UseStaticFiles();

app.UseAuthorization();

//Allow CORS
app.UseCors("CorsPolicy");

app.MapControllers();

/* Method()
 *This method is used to create a scope for the application
This scope is used to create a new instance of the database
This instance is used to create the database if it doesn't exist
This instance is used to migrate the database if there are any pending migrations 
 */
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    //Migrate changes to the database
    await context.Database.MigrateAsync();
    //Seed the database with data
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception e)
{
    logger.LogError(e, "An error occurred during migration");
}

app.Run();