namespace e_Estoque_API.Core.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
