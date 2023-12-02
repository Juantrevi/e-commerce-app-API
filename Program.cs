//Creates an instance of the web application using the default settings and runs it.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container. Add any additional services to the container here like the following:
// builder.Services.AddSingleton<IExampleService, ExampleService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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