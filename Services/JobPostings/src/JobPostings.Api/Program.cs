using FluentValidation.AspNetCore;
using JobPostings.Api.Common.ErrorHandler;
using JobPostings.Api.Common.Extensions.DependencyInjection;
using JobPostings.Api.Jobs;
using JobPostings.Application.Consumers;
using JobPostings.Application.Mapping;
using JobPostings.Application.Validations;
using MassTransit;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddValidators();
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

builder.Services.AddCronJobs();

builder.Services.AddControllers()
       .AddFluentValidation(opts =>
       {
           opts.RegisterValidatorsFromAssemblyContaining<CreateJobPostingRequestValidator>();
           opts.ImplicitlyValidateChildProperties = true;
       });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAutoMapper(typeof(JobPostingProfile));

builder.Services.AddJwtAuthentication(builder.Configuration);
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
