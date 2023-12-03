//Creates an instance of the web application using the default settings and runs it.

using Core.Interfaces;
using Infrastructure.Data;
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

//AddScoped:
//A new instance is created for each request, only for the duration of that request
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Swagger is a tool that can help you generate documentation for your API.
    //https://localhost:5001/swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();