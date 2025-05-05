using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TaskManager.API.Middlewares;
using TaskManager.API.Settings;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskManagerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IWorkspaceService, WorkspaceService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<BusinessLogic.Profiles.TaskProfile>();
    cfg.AddProfile<DataAccess.Profiles.TaskProfile>();
    cfg.AddProfile<BusinessLogic.Profiles.WorkspaceProfile>();
    cfg.AddProfile<DataAccess.Profiles.WorkspaceProfile>();
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddCors(options =>
{
    var allowedCorsOrigins = builder.Configuration.GetSection("AllowedCors:Origins").Get<string[]>();
    if (allowedCorsOrigins == null || allowedCorsOrigins.Length == 0)
    {
        throw new InvalidOperationException("Allowed CORS origins are not configured in appsettings.json.");
    }
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedCorsOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>() ??
    throw new InvalidOperationException("Missing 'JwtOptions' configuration in appsettings.json.");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = jwtOptions.Authority; // Keycloak Realm URL
        options.Audience = jwtOptions.Audience; // Keycloak Client ID
        options.RequireHttpsMetadata = jwtOptions.RequireHttpsMetadata;
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
