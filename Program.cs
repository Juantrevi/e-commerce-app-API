//Creates an instance of the web application using the default settings and runs it.

using Core.Interfaces;
using e_commerce_app.Errors;
using e_commerce_app.Middleware;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Adding a connection string to the database (SQLite)
builder.Services.AddDbContext<StoreContext>(opt 
    => opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container
// Add any additional services to the container here like the following:
// builder.Services.AddSingleton<IExampleService, ExampleService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//This is how we add a generic repository to the container
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//ApiValidationErrorResponse is a class that will be returned to the client when there is a validation error
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage).ToArray();

        var errorResponse = new ApiValidationErrorResponse
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});

//AddScoped:
//A new instance is created for each request, only for the duration of that request
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//Middleware to redirect users to ErrorController when an error occurs
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

//if (app.Environment.IsDevelopment())
//{

/*
 *  Swagger
    is a tool that can help you generate documentation for your API.
 *  https://localhost:5001/swagger
    Make it available for production and development environments
 */
app.UseSwagger();
app.UseSwaggerUI();
//}
//This method is used to serve static files
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

//This method is used to create a scope for the application
//This scope is used to create a new instance of the database
//This instance is used to create the database if it doesn't exist
//This instance is used to migrate the database if there are any pending migrations
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