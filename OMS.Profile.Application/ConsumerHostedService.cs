using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OMS.Profile.Application.Common.Extensions;
using OMS.Profile.Application.Common.Interfaces;
using OMS.Profile.Application.Common.Settings;

namespace OMS.Profile.Application;

public class ConsumerHostedService<TMessage> : IHostedService, IDisposable where TMessage : class
{
    private readonly IConsumer<TMessage> _myConsumer;
    private readonly IConsumer<Ignore, string> _consumer;

    public ConsumerHostedService(IConsumer<TMessage> myConsumer, IOptions<KafkaSettings> options)
    {
        _myConsumer = myConsumer;
        var settings = options.Value;
        var config = new ConsumerConfig()
        {
            BootstrapServers = settings.BootstrapServer,
            GroupId = settings.GroupId,
        };
        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() =>
        {
            _consumer.Subscribe(typeof(TMessage).Name);
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(cancellationToken);
                var message = result.Message.Value.DeserializeJson<TMessage>();
                _myConsumer.Handle(message, cancellationToken);
            }
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Close();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _consumer.Dispose();
    }
}