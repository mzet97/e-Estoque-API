using e_Estoque_API.Core.Events;
using e_Estoque_API.Domain.Entities;

namespace e_Estoque_API.Core.Entities;

public class AggregateRoot : Entity
{
    protected AggregateRoot()
    {
    }
    protected AggregateRoot(
        Guid id,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt,
        bool isDeleted) : base(id, createdAt, updatedAt, deletedAt, isDeleted)
    {
    }
}
