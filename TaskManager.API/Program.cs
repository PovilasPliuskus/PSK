using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TaskManager.API.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskManagerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddTransient<IWorkspaceService, WorkspaceService>();
builder.Services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();

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

var app = builder.Build();

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
