namespace OMS.Profile.Application.Common.IntegrationEvents.Events;

public class ByeEvent
{
    public ByeEvent(DateTime time)
    {
        Message = "Bye";
        Time = time;
    }

    public string Message { get; }
    public DateTime Time { get; }
}