using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Application.Inventories.ViewModels;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Inventories.Queries.Handlers;

public class GetByIdInventoryQueryHandler : IRequestHandler<GetByIdInventoryQuery, BaseResult<InventoryViewModel>>
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IMessageBusClient _messageBus;

    public GetByIdInventoryQueryHandler(
        IInventoryRepository inventoryRepository,
        IMessageBusClient messageBus)
    {
        _inventoryRepository = inventoryRepository;
        _messageBus = messageBus;
    }

    public async Task<BaseResult<InventoryViewModel>> Handle(
        GetByIdInventoryQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _inventoryRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Not found Inventory", "Not found Inventory");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Not found");
        }

        return new BaseResult<InventoryViewModel>(InventoryViewModel.FromEntity(entity), true);
    }
}