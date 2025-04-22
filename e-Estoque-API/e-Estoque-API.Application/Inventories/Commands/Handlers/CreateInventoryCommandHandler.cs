using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Inventories.Commands.Handlers;

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Guid>
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMessageBusClient _messageBus;

    public CreateInventoryCommandHandler(
        IInventoryRepository inventoryRepository,
        IProductRepository productRepository,
        IMessageBusClient messageBus
        )
    {
        _inventoryRepository = inventoryRepository;
        _productRepository = productRepository;
        _messageBus = messageBus;
    }

    public async Task<Guid> Handle(
        CreateInventoryCommand request,
        CancellationToken cancellationToken)
    {
        var entity = Inventory.Create(request.Quantity, request.DateOrder, request.IdProduct);

        if (!entity.IsValid())
        {
            var errors = String.Join(",", entity.GetErrors());
            var noticiation = new NotificationError("Validate Inventory has error", errors);
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new ValidationException(errors);
        }

        var product = await _productRepository.GetByIdAsync(request.IdProduct);

        if (product == null)
        {
            var noticiation = new NotificationError("Product not found", "Product not found");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Product not found");
        }

        await _inventoryRepository.AddAsync(entity);

        foreach (var @event in entity.Events)
        {
            var routingKey = @event.GetType().Name.ToDashCase();

            _messageBus.Publish(@event, routingKey, "inventory-service");
        }

        return entity.Id;
    }
}