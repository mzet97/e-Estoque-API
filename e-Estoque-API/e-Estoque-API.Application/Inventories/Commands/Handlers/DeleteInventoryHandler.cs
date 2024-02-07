using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Inventories.Commands.Handlers
{
    public class DeleteInventoryHandler : IRequestHandler<DeleteInventoryCommand, Unit>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMessageBusClient _messageBus;

        public DeleteInventoryHandler(IInventoryRepository inventoryRepository, IMessageBusClient messageBus)
        {
            _inventoryRepository = inventoryRepository;
            _messageBus = messageBus;
        }

        public async Task<Unit> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _inventoryRepository.GetById(request.Id);

            if (entity == null)
            {
                var noticiation = new NotificationError("Delete Inventory has error", "Delete Inventory has error");
                var routingKey = noticiation.GetType().Name.ToDashCase();

                _messageBus.Publish(noticiation, routingKey, "noticiation-service");

                throw new NotFoundException("Delete Error");
            }

            await _inventoryRepository.Remove(entity.Id);

            return Unit.Value;
        }
    }
}
