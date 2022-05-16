using Common.Abstractions;
using JobPostings.Api.Extensions;
using JobPostings.Application.Commands.PostJob;
using JobPostings.Infra.Data;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

var dbConnection = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<ModelContext>(cfg => cfg.UseNpgsql(dbConnection));

var rabbitMqConnection = builder.Configuration.GetConnectionString("RabbitMq");
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
        config.Host(new Uri(rabbitMqConnection));
    });
});

builder.Services.AddMediatR(typeof(PostJobCommand));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorizationPolicies();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }