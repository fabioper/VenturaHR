using System.Reflection;
using FluentValidation.AspNetCore;
using JobPostings.Api.Extensions.DI;
using JobPostings.Application.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddDbContext(builder.Configuration);

var rabbitMqConnection = builder.Configuration.GetConnectionString("RabbitMq");
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CompanyCreatedConsumer>();
    x.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
        config.Host(new Uri(rabbitMqConnection));
    });
});

builder.Services.AddControllers()
       .AddFluentValidation(opts => opts.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorizationPolicies();

builder.Services.AddCorsPolicy(out var policy);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(policy);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }