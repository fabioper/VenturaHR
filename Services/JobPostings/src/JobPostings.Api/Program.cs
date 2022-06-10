using FluentValidation.AspNetCore;
using JobPostings.Api.Common.ErrorHandler;
using JobPostings.Api.Extensions.DependencyInjection;
using JobPostings.Application.Consumers;
using JobPostings.Application.Mapping;
using JobPostings.Application.Validations;
using JobPostings.CrossCutting.Settings;
using MassTransit;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Settings

var tokenSettings = builder.Configuration
    .GetSection(nameof(TokenSettings))
    .Get<TokenSettings>();

var emailSettings = builder.Configuration
    .GetSection(nameof(EmailSettings))
    .Get<EmailSettings>();

var connectionStrings = builder.Configuration
    .GetSection(nameof(ConnectionStrings))
    .Get<ConnectionStrings>();

builder.Services.AddSingleton(tokenSettings);
builder.Services.AddSingleton(emailSettings);
builder.Services.AddSingleton(connectionStrings);

#endregion

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddValidators();
builder.Services.AddDbContext(connectionStrings.Database);
builder.Services.AddCronJobs();

builder.Services.AddControllers().AddFluentValidation(opts =>
{
    opts.RegisterValidatorsFromAssemblyContaining<CreateJobPostingRequestValidator>();
    opts.ImplicitlyValidateChildProperties = true;
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CompanyCreatedConsumer>();
    x.AddConsumer<ApplicantCreatedConsumer>();

    x.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
        config.Host(new Uri(connectionStrings.RabbitMq));
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAutoMapper(typeof(JobPostingProfile));

builder.Services.AddJwtAuthentication(tokenSettings);
builder.Services.AddPolicies();

var app = builder.Build();

app.UseMiddleware<ApiExceptionMiddleware>();

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
