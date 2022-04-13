using MassTransit;
using Users.ConfigOptions;
using Users.Services.Concretes;
using Users.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(cfg => cfg.UsingRabbitMq());

builder.Services.AddTransient(
    o => builder.Configuration.GetSection(
        KeycloakOptions.Keycloack).Get<KeycloakOptions>());

builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddHttpClient<IKeycloakClient, KeycloakClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();