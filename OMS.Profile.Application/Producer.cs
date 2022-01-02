using Confluent.Kafka;
using Microsoft.Extensions.Options;
using OMS.Profile.Application.Common.Extensions;
using OMS.Profile.Application.Common.Settings;

namespace OMS.Profile.Application;

public interface IProducer
{
    Task ProduceAsync<T>(T message);
}

public class Producer : IProducer, IDisposable
{
    private readonly IProducer<Null, string> _producer;

    public Producer(IOptionsSnapshot<KafkaSettings> options)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = options.Value.BootstrapServer,
        };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task ProduceAsync<TMessage>(TMessage message)
    {
        var dr = await _producer.ProduceAsync(typeof(TMessage).Name, new Message<Null, string>
        {
            Value = message.ToJsonString()
        });
    }

    public void Dispose()
    {
        _producer.Flush();
        _producer.Dispose();
    }
}