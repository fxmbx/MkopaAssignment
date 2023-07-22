using MessageQueueLibrary;
using MessageQueueLibrary.Contracts;
using SmsService.Api.Extension;
using SmsService.Infrastructure.Events.Handler;
using SmsService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddSingleton(typeof(ICustomProducer<,>), typeof(CustomProducer<,>));
builder.Services.AddSingleton(typeof(ICustomConsumer<,>), typeof(CustomConsumer<,>));

builder.Services.AddScoped<ISmsService, SmsServiceImp>();
builder.Services.AddScoped<ICustomHandler<string, SmsPayload>, SmsHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRequestLoggingMiddleware();


app.Run();
