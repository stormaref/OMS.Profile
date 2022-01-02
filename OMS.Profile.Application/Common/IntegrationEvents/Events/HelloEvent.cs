namespace OMS.Profile.Application.Common.IntegrationEvents.Events;

public class HelloEvent
{
    public HelloEvent(DateTime time)
    {
        Message = "Hello";
        Time = time;
    }

    public string Message { get; }
    public DateTime Time { get; }
}