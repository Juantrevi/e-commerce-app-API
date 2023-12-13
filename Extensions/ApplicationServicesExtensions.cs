using Core.Interfaces;
using e_commerce_app.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_app.Extensions;

/*
 * Class ApplicationServicesExtensions
 * This class is used to extend the IServiceCollection interface,
 * this way we can clean up the code in the Program.cs file
 * and make it more readable and maintainable as all the
 * services are registered in this class
 */
public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
    /*
    Connection to DB
    Adding a connection string to the database (SQLite) 
    */
    services.AddDbContext<StoreContext>(opt 
        => opt.UseSqlite(config.GetConnectionString("DefaultConnection")));
    
    /*
    Add services to the container
    Add any additional services to the container here like the following:
    builder.Services.AddSingleton<IExampleService, ExampleService>();
    */
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    
    /*
     Adding a generic repository to the container 
    */
    services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    
    /*
     ApiValidationErrorResponse
     is a class that will be returned to the client when there is a validation error
    */
    services.Configure<ApiBehaviorOptions>(options =>
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

    /*
    AddScoped()
      A new instance is created for each request, only for the duration of that request 
    */
    services.AddScoped<IProductRepository, ProductRepository>();
    
    /*
     Adding CORS support
     */
    services.AddCors(opt =>
    {
        opt.AddPolicy("CorsPolicy", policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
        });
    });
    
    
    return services;
    }
}