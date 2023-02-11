using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderService.Configs;
using OrderService.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure DBContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add MediatR
builder.Services.AddMediatR(typeof(Program).Assembly);

// Add Configs
builder.Services.Configure<RabbitMqEndPointsConfig>(builder.Configuration.GetSection("RabbitMqEndPoints"));

// Add MassTransit
builder.Services.AddMassTransit(x => x.UsingRabbitMq());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
