namespace e_Estoque_API.Core.Events
{
    public class NotificationError : IDomainEvent
    {
        public string EventName { get; private set; }
        public string Description { get; private set; }

        public NotificationError(string eventName, string description)
        {
            EventName = eventName;
            Description = description;
        }
    }
}
