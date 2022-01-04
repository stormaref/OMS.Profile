using KafkaStorm.Interfaces;
using Microsoft.Extensions.Logging;
using OMS.Profile.Application.Common.IntegrationEvents.Events;

namespace OMS.Profile.Application.Common.IntegrationEvents.EventConsumers;

public class HelloConsumer : IConsumer<HelloEvent>
{
    private readonly ILogger<HelloConsumer> _logger;

    public HelloConsumer(ILogger<HelloConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Handle(HelloEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogError("Test");
        Console.WriteLine($"{@event.Message} from {DateTime.Now.Subtract(@event.Time).TotalMilliseconds} ms ago");
    }
}