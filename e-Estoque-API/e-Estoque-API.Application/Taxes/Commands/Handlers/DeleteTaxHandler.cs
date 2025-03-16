using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Taxes.Commands.Handlers;

public class DeleteTaxHandler : IRequestHandler<DeleteTaxCommand, Unit>
{
    private readonly ITaxRepository _taxRepository;
    private readonly IMessageBusClient _messageBus;

    public DeleteTaxHandler(ITaxRepository taxRepository, IMessageBusClient messageBus)
    {
        _taxRepository = taxRepository;
        _messageBus = messageBus;
    }

    public async Task<Unit> Handle(
        DeleteTaxCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _taxRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Delete Tax has error", "Delete Tax has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Delete Error");
        }

        await _taxRepository.RemoveAsync(entity.Id);

        return Unit.Value;
    }
}