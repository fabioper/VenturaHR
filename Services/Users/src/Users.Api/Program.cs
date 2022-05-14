using Common.Abstractions;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Api.Commands;
using Users.Api.Commands.CreateCompany;
using Users.Api.Data;
using Users.Api.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnection = builder.Configuration.GetConnectionString("Database");
var rabbitMqConnection = builder.Configuration.GetConnectionString("RabbitMq");

builder.Services.AddDbContext<UsersContext>(c => c.UseNpgsql(dbConnection));


builder.Services.AddMassTransit(
    x =>
    {
        x.UsingRabbitMq(
            (context, config) =>
            {
                config.ConfigureEndpoints(context);
                config.Host(new Uri(rabbitMqConnection));
            }
        );
    }
);


builder.Services.AddMediatR(typeof(CreateCompanyCommand));
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

var app = builder.Build();

using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
var context = serviceScope?.ServiceProvider.GetRequiredService<UsersContext>();

if (context?.Database.IsNpgsql() == true)
    context.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();