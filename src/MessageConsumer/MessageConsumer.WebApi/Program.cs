using MessageConsumer.Infrastructure.Services;
using MessageConsumer.Infrastructure.Services.Implementations;
using MessageConsumer.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<MessageBackgroundConsumer>();

builder.Services.AddSingleton<IResultProcessingService, ResultProcessingService>();
builder.Services.AddSingleton<IMessageHandlerService, MessageHandlerService>();
builder.Services.AddSingleton<IRabbitMqListenerService, RabbitMqListenerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();