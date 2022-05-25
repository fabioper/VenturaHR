using System.Reflection;
using FluentValidation.AspNetCore;
using JobPostings.Api.Extensions.DI;
using JobPostings.Application.Consumers;
using JobPostings.Application.Mapping;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    var connection = builder.Configuration.GetConnectionString("RabbitMq");

    x.AddConsumer<CompanyCreatedConsumer>();
    x.AddConsumer<ApplicantCreatedConsumer>();

    x.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
        config.Host(new Uri(connection));
    });
});

builder.Services.AddControllers()
       .AddFluentValidation(opts => opts.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAutoMapper(typeof(JobPostingProfile));

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddPolicies();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
