using System.Reflection;
using Common.Abstractions;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Api.Commands.CreateCompany;
using Users.Api.Data;
using Users.Api.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
       .AddFluentValidation(x =>
       {
           x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
           x.ImplicitlyValidateChildProperties = true;
       });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnection = builder.Configuration.GetConnectionString("Database");
var rabbitMqConnection = builder.Configuration.GetConnectionString("RabbitMq");

builder.Services.AddDbContext<UsersContext>(c => c.UseNpgsql(dbConnection));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
        config.Host(new Uri(rabbitMqConnection));
    });
});

builder.Services.AddMediatR(typeof(CreateCompanyCommand));
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

const string corsConfig = "_corsConfig";

builder.Services.AddCors(config =>
{
    config.AddPolicy(corsConfig, policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors(corsConfig);

app.UseAuthorization();

app.MapControllers();

app.Run();