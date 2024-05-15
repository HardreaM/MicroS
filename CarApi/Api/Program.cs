using CarApi;
using Dal;
using Dal.Cars;
using Logic;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Services;

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

var rabbitSettings = new RabbitProfileConnectionSettings(builder.Configuration);
builder.Services.AddTransient(_ => rabbitSettings);
builder.Services.AddHostedService<RpcConnectionServer>();

builder.Services.AddMassTransit(cfg =>
    {
        cfg.SetKebabCaseEndpointNameFormatter();
        cfg.AddDelayedMessageScheduler();
        cfg.AddConsumer<ChangeUserNameConsumer>();
        cfg.UsingRabbitMq((brc, rbfc) =>
        {
            rbfc.UseInMemoryOutbox();
            rbfc.UseMessageRetry(r =>
            {
                r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            });
            rbfc.UseDelayedMessageScheduler();
            rbfc.Host("localhost", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            rbfc.ConfigureEndpoints(brc);
        });
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();