using CarApi;
using Dal;
using Dal.Cars;
using Logic;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var setting = new DalSettings(builder.Configuration);
builder.Services.AddTransient(_ => setting);
builder.Services.AddDbContext<CarInfoContext>(options => options.UseNpgsql(setting.ConnectionString));

builder.Services.TryAddLogic();
builder.Services.TryAddDal();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();