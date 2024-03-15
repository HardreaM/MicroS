using Core.HttpLogic;
using Core.Logs;
using Domain.Interfaces;
using Infastracted;
using Infastracted.Connections;
using Infastracted.Data;
using Microsoft.EntityFrameworkCore;
using ProfileConnectionLib;
using ProfileConnectionLib.ConnectionServices;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using Serilog;
using Services;
using Services.Intefaces;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseSerilog((hostingContext, loggerConfig) =>
{
    loggerConfig.GetConfiguration();
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpRequestService();
builder.Services.AddRabbitProfileConnectionService();

builder.Services.AddRepository(builder.Configuration);
builder.Services.AddProfileService(builder.Configuration);
builder.Services.AddCreatePost();
builder.Services.AddCheckUser();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
