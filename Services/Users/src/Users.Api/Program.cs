using System.Reflection;
using FluentValidation.AspNetCore;
using Users.Api.Common.ErrorHandler;
using Users.Api.Common.Extensions;
using Users.Application.Mappings;
using Users.Application.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
       .AddFluentValidation(x =>
       {
           x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
           x.ImplicitlyValidateChildProperties = true;
       });

builder.Services.AddEndpointsApiExplorer();

var tokenSettings = builder.Configuration.GetSection(nameof(TokenSettings)).Get<TokenSettings>();
builder.Services.AddScoped(_ => tokenSettings);

builder.Services.ConfigureJwt(tokenSettings);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureRedis(builder.Configuration);
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.AddAutoMapper(x => x.AddProfile<UserProfile>());

builder.Services.AddRepositories();
builder.Services.AddServices();

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

app.UseMiddleware<ApiExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors(corsConfig);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();