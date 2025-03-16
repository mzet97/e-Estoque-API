namespace e_Estoque_API.Domain.Entities;

public interface IEntity
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
    DateTime? DeletedAt { get; }
    bool IsDeleted { get; }
}
