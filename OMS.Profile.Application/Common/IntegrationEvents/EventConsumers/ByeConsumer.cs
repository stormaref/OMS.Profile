using OMS.Profile.Application.Common.IntegrationEvents.Events;
using OMS.Profile.Application.Common.Interfaces;

namespace OMS.Profile.Application.Common.IntegrationEvents.EventConsumers;

public class ByeConsumer : IConsumer<ByeEvent>
{
    public async Task Handle(ByeEvent @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{@event.Message} from {DateTime.Now.Subtract(@event.Time).TotalMilliseconds} ms ago");
    }
}