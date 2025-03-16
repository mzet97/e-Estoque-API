using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Inventories.Commands.Handlers;

public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Guid>
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMessageBusClient _messageBus;

    public UpdateInventoryCommandHandler(
        IInventoryRepository inventoryRepository,
        IProductRepository productRepository,
        IMessageBusClient messageBus)
    {
        _inventoryRepository = inventoryRepository;
        _productRepository = productRepository;
        _messageBus = messageBus;
    }

    public async Task<Guid> Handle(
        UpdateInventoryCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _inventoryRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Update Inventory has error", "Update Inventory has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Find Error");
        }

        entity.Update(
            request.Quantity,
            request.DateOrder,
            request.IdProduct);

        if (!entity.IsValid())
        {
            var noticiation = new NotificationError("Validate Inventory has error", "Validate Inventory has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new ValidationException("Validate Error");
        }

        var product = await _productRepository.GetByIdAsync(request.IdProduct);

        if (product == null)
        {
            var noticiation = new NotificationError("Product not found", "Product not found");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Product not found");
        }

        await _inventoryRepository.UpdateAsync(entity);

        foreach (var @event in entity.Events)
        {
            var routingKey = @event.GetType().Name.ToDashCase();

            _messageBus.Publish(@event, routingKey, "inventory-service");
        }

        return entity.Id;
    }
}