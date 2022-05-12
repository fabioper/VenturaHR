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

var dbConnection = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<UsersContext>(c =>
    c.UseNpgsql(dbConnection));

var rabbitConfig = builder.Configuration.GetSection(RabbitMqConfig.RabbitMq)
                          .Get<RabbitMqConfig>();
builder.Services.AddMassTransit(c =>
{
    c.UsingRabbitMq((ctx, config) =>
    {
        config.Host(rabbitConfig.Host, rabbitConfig.Port, rabbitConfig.Username, h =>
        {
            h.Username(rabbitConfig.Username);
            h.Password(rabbitConfig.Password);
            h.UseSsl(s => s.Protocol = SslProtocols.Tls12);
        });
    });
});

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