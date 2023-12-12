using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RentCarWorkship.Models.Base;
using FluentMigrator.Runner;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.ClaimsIssuer = AuthOptions.Issuer;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.Issuer,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.Audience,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }

                return Task.CompletedTask;
            }
        };
    });
var connectionString = builder.Configuration.GetSection("Postgres").Value;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<DapperContext>();
// builder.Services.AddSingleton<IConfigurationSettings, ConfigurationSettings>();
//
// builder.Services.AddScoped<IAccountRepository, AccountRepository>();
// builder.Services.AddScoped<IAccountService, AccountService>();
// builder.Services.AddScoped<IJwtService, JwtService>();

var app = builder.Build();
using var serviceprovider = app.Services.CreateScope();
var services = serviceprovider.ServiceProvider;
var runner = services.GetRequiredService<IMigrationRunner>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();