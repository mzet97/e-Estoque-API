using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Customers.Commands.Handlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMessageBusClient _messageBus;

    public CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IMessageBusClient messageBus)
    {
        _customerRepository = customerRepository;
        _messageBus = messageBus;
    }

    public async Task<Guid> Handle(
        CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var entity = Customer.Create(
            request.Name,
            request.DocId,
            request.Email,
            request.Description,
            request.PhoneNumber,
            request.CustomerAddress);

        if (!entity.IsValid())
        {
            var noticiation = new NotificationError("Validate Customer has error", "Validate Customer has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new ValidationException("Validate Error");
        }

        await _customerRepository.AddAsync(entity);

        foreach (var @event in entity.Events)
        {
            var routingKey = @event.GetType().Name.ToDashCase();

            _messageBus.Publish(@event, routingKey, "customer-service");
        }

        return entity.Id;
    }
}