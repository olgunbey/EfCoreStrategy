using ConnectionResiliency.DbContexts;
using ConnectionResiliency.Models;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(conf =>
{
    ConnectionFactory connectionFactory = new()
    {
        HostName = "localhost",
        UserName = "olgun",
        Password = "1905olgun",
        Port =1453
    };
    return connectionFactory.CreateConnection().CreateModel();
});

builder.Services.AddScoped(typeof(IException<>), typeof(ExceptionClass<>));

builder.Services.AddScoped<IConnectionResiliency, ConnectionResiliencyClass>();
builder.Services.AddDbContext<ExampleDbContext>(x=>x.UseSqlServer("Server=127.0.0.1,4149;Database=ExampleDb;User Id=SA;Password=Password123;TrustServerCertificate=True", (builder) =>
{
    builder.ExecutionStrategy((dependencies) =>
    {
        return new ExecutedStrategy(dependencies,1,TimeSpan.FromSeconds(10));
    });
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
