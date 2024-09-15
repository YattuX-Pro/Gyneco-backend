using Gyneco.Identity;
using Gyneco.Infrastructure;
using Gyneco.Persistence;
using Gyneco.Application;
using Serilog;
using Gyneco.Api.Middleware;
using Gyneco.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
    .WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200", builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .WithOrigins("http://localhost:4200")
               .AllowCredentials();
    });
});

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost4200"); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
