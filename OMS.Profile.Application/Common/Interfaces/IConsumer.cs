namespace OMS.Profile.Application.Common.Interfaces;

public interface IConsumer<in T> where T : class
{
    Task Handle(T message, CancellationToken cancellationToken);
}