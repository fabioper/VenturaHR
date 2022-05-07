using System.Security.Authentication;
using Common.Config;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var rabbitMqConfig = builder.Configuration.GetSection(RabbitMqConfig.RabbitMq).Get<RabbitMqConfig>();

builder.Services.AddMassTransit(opts =>
{
    opts.UsingRabbitMq((_, config) =>
    {
        config.Host(rabbitMqConfig.Host, rabbitMqConfig.Port, rabbitMqConfig.Username, h =>
        {
            h.Username(rabbitMqConfig.Username);
            h.Password(rabbitMqConfig.Password);

            h.UseSsl(s => s.Protocol = SslProtocols.Tls12);
        });
    });
});

const string corsConfig = "_corsConfig";
builder.Services.AddCors(config =>
{
    config.AddPolicy(corsConfig, c =>
    {
        c.WithOrigins("http://localhost:3000", "https://venturahr-api-gateway.herokuapp.com")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors(corsConfig);
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();