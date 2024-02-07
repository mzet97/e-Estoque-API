using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Customers.Commands.Handlers;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMessageBusClient _messageBus;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IMessageBusClient messageBus)
    {
        _customerRepository = customerRepository;
        _messageBus = messageBus;
    }

    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _customerRepository.GetById(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Delete Customer has error", "Delete Customer has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Delete Error");
        }

        await _customerRepository.Remove(entity.Id);

        return Unit.Value;
    }
}