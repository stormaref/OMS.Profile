using KafkaStorm.Interfaces;
using OMS.Profile.Application.Common.IntegrationEvents.Events;

namespace OMS.Profile.Application.Common.IntegrationEvents.EventConsumers;

public class ByeConsumer : IConsumer<ByeEvent>
{
    public async Task Handle(ByeEvent @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{@event.Message} from {DateTime.Now.Subtract(@event.Time).TotalMilliseconds} ms ago");
    }
}