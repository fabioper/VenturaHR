using System.Reflection;
using System.Security.Authentication;
using Common.Config;
using Common.Extensions;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Api.Data;
using Users.Api.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnection = builder.Configuration.GetConnectionString("Database");
var rabbitMqConnection = builder.Configuration.GetConnectionString("RabbitMq");

builder.Services.AddDbContext<UsersContext>(c =>
    c.UseNpgsql(dbConnection));

builder.Services.AddMassTransit(c =>
    c.UsingRabbitMq((_, config) =>
        config.Host(new Uri(rabbitMqConnection))));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(c => c.AddProfile<UserMapping>());
builder.Services.AddCommon();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();