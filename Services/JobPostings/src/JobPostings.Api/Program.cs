using System.Reflection;
using FluentValidation.AspNetCore;
using JobPostings.Api.Extensions;
using JobPostings.Application.Consumers;
using JobPostings.Application.Services.Concretes;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.CompanyAggregate;
using JobPostings.Domain.JobPostingAggregate;
using JobPostings.Infra.Data;
using JobPostings.Infra.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IJobPostingRepository, JobPostingRepository>();
builder.Services.AddScoped<IJobPostingsService, JobPostingsService>();

var dbConnection = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<ModelContext>(cfg => cfg.UseNpgsql(dbConnection));

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

builder.Services
       .AddControllers()
       .AddFluentValidation(opts =>
           opts.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorizationPolicies();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }