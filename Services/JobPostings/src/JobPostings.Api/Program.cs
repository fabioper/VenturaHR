using Common.Abstractions;
using JobPostings.Api.Extensions;
using JobPostings.Application.Commands.PostJob;
using JobPostings.Domain.CompanyAggregate;
using JobPostings.Domain.JobPostingAggregate;
using JobPostings.Infra.Data;
using JobPostings.Infra.Repositories;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IJobPostingRepository, JobPostingRepository>();

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