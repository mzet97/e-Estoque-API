using e_Estoque_API.Domain.Events;

namespace e_Estoque_API.Core.Events;

public class NotificationError : DomainEvent
{
    public string EventName { get; private set; }
    public string Description { get; private set; }

    public NotificationError(string eventName, string description)
    {
        EventName = eventName;
        Description = description;
    }
}