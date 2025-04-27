using MessageProducer.Infrastructure.Services;
using MessageProducer.Infrastructure.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();

var app = builder.Build();

// Enable Swagger in Development (or always for now)
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();

app.Run();