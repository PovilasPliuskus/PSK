using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Sso;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskManagerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.ConfigureSso(builder.Configuration.GetSection("AuthenticationValues").Get<AuthenticationValues>() ?? 
                              throw new InvalidOperationException("Missing 'AuthenticationValues' configuration in appsettings.json."));

var app = builder.Build();

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
