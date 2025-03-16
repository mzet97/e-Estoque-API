using e_Estoque_API.Core.Events;

namespace e_Estoque_API.Domain.Events;

public abstract class DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; protected set; }

    protected DomainEvent()
    {
        OccurredOn = DateTime.UtcNow;
    }
}