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
using DataAccess.Extensions;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskManagerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IWorkspaceService, WorkspaceService>();
builder.Services.AddTransient<ISubTaskService, SubTaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
builder.Services.AddScoped<ISubTaskRepository, SubTaskRepository>();

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<BusinessLogic.Profiles.TaskProfile>();
    cfg.AddProfile<DataAccess.Profiles.TaskProfile>();
    cfg.AddProfile<BusinessLogic.Profiles.WorkspaceProfile>();
    cfg.AddProfile<DataAccess.Profiles.WorkspaceProfile>();
    cfg.AddProfile<BusinessLogic.Profiles.SubTaskProfile>();
    cfg.AddProfile<DataAccess.Profiles.SubTaskProfile>();
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

builder.Services.AddSerilog();

var app = builder.Build();

if (builder.Configuration.GetValue<bool>("UseRequestLogging")) {
    app.UseSerilogRequestLogging(options => {
        options.GetMessageTemplateProperties = (httpContext, requestPath, elapsedMs, statusCode) =>
            new[] {
                new LogEventProperty("StatusCode", new ScalarValue(statusCode)),
                new LogEventProperty("RequestMethod", new ScalarValue(httpContext.Request.Method)),
                new LogEventProperty("RequestPath", new ScalarValue(requestPath)),
                new LogEventProperty("RequestController", new ScalarValue(httpContext.GetRouteData().Values["controller"])),
                new LogEventProperty("ControllerAction", new ScalarValue(httpContext.GetRouteData().Values["action"])),
                new LogEventProperty("UserEmail", new ScalarValue(httpContext.User.GetUserEmail())),
            };
        options.MessageTemplate = "{StatusCode} HTTP {RequestMethod} {RequestPath}, {RequestController} {ControllerAction} {UserEmail}";
        
        var columnWriters = new Dictionary<string, ColumnWriterBase>{
            {"StatusCode", new SinglePropertyColumnWriter("StatusCode") },
            {"RequestMethod", new SinglePropertyColumnWriter("RequestMethod") },
            {"RequestPath", new SinglePropertyColumnWriter("RequestPath") },
            {"RequestController", new SinglePropertyColumnWriter("RequestController") },
            {"ControllerAction", new SinglePropertyColumnWriter("ControllerAction") },
            {"UserEmail", new SinglePropertyColumnWriter("UserEmail") },
        };
        
        options.Logger = new LoggerConfiguration()
            .WriteTo.PostgreSQL(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                "RequestLog",
                columnWriters,
                needAutoCreateTable: true,
                respectCase: true
            )
            .CreateLogger();
    });
}

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
