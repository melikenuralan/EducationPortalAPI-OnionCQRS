using EducationPortal.Application;
using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Infrastructure;
using EducationPortal.Persistance;
using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>

//        policy.WithOrigins(/*"http://localhost:5001*/", "/*http://localhost:5000*/", "/*http://localhost:x*/")
//              .AllowAnyHeader()
//              .AllowAnyMethod()
//              .AllowCredentials()// ????
//));

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Education Portal API", Version = "v1", Description = "Education Portal API swagger client." });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the token after typing 'Bearer' followed by a space. \r\n\r\n For Instance: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }

    });
});
builder.Services.AddMemoryCache();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            NameClaimType = ClaimTypes.Name,
        };
    });

builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddApplication();

var env = builder.Environment;
builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

string hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");

builder.Services.AddHangfire(configuration =>
    configuration.UseStorage(new MySqlStorage(hangfireConnectionString, new MySqlStorageOptions())));


builder.Services.AddInfrastructureServices();

builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseCors();
app.UseHangfireDashboard("/mydashboard");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/get-userid", (ICurrentUserService currentUser) => currentUser.UserId)
     .RequireAuthorization();

app.MapGet("/get-username", (ICurrentUserService currentUser) => currentUser.Username)
    .RequireAuthorization();
app.Run();
