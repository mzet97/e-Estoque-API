using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Products.Commands.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMessageBusClient _messageBus;

        public DeleteProductHandler(IProductRepository productRepository, IMessageBusClient messageBus)
        {
            _productRepository = productRepository;
            _messageBus = messageBus;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetById(request.Id);

            if (entity == null)
            {
                var noticiation = new NotificationError("Delete Product has error", "Delete Product has error");
                var routingKey = noticiation.GetType().Name.ToDashCase();

                _messageBus.Publish(noticiation, routingKey, "noticiation-service");

                throw new NotFoundException("Delete Error");
            }

            await _productRepository.Remove(entity.Id);

            return Unit.Value;
        }
    }
}
