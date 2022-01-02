using Confluent.Kafka;
using OMS.Profile.API.Common.Extensions;
using OMS.Profile.Application;
using OMS.Profile.Application.Common.IntegrationEvents.EventConsumers;
using OMS.Profile.Application.Common.IntegrationEvents.Events;
using OMS.Profile.Application.Common.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigSeq();
builder.Services.AddOptions();
builder.AddSettings<KafkaSettings>();
builder.Services.AddScoped<IProducer, Producer>();
builder.Services.AddConsumer<HelloConsumer, HelloEvent>();
builder.Services.AddConsumer<ByeConsumer, ByeEvent>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();