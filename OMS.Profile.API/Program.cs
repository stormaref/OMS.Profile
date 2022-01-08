using Confluent.Kafka;
using KafkaStorm.Extensions;
using OMS.Profile.API.Common.Extensions;
using OMS.Profile.API.Common.Settings;
using OMS.Profile.Application.Common.IntegrationEvents.EventConsumers;
using OMS.Profile.Application.Common.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigSeq();
// builder.Services.AddOptions();
// builder.AddSettings<KafkaSettings>();

var kafkaSettings = builder.Configuration.GetSection(nameof(KafkaSettings)) as KafkaSettings;

builder.Services.AddKafkaStorm(factory =>
{
    factory.SetConsumerConfig(new ConsumerConfig()
    {
        BootstrapServers = kafkaSettings.BootstrapServer,
        GroupId = kafkaSettings.GroupId
    });

    factory.AddProducer(new ProducerConfig()
    {
        BootstrapServers = kafkaSettings.BootstrapServer,
    });

    factory.AddConsumer<HelloConsumer, HelloEvent>();
    factory.AddConsumer<ByeConsumer, ByeEvent>();
});

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