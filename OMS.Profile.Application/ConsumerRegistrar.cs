using Microsoft.Extensions.DependencyInjection;
using OMS.Profile.Application.Common.Interfaces;

namespace OMS.Profile.Application;

public static class ConsumerRegistrar
{
    public static void AddConsumer<TConsumer, TMessage>(this IServiceCollection services)
        where TMessage : class
        where TConsumer : class, IConsumer<TMessage>
    {
        services.AddTransient<IConsumer<TMessage>, TConsumer>();
        services.AddHostedService<ConsumerHostedService<TMessage>>();
    }
}