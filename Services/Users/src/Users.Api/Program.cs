using System.Reflection;
using Common;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Api.Data;
using Users.Api.Data.Repositories;
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
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

var app = builder.Build();

using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
var context = serviceScope?.ServiceProvider.GetRequiredService<UsersContext>();
context?.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();