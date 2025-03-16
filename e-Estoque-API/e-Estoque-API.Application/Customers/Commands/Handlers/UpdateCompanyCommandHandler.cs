using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Customers.Commands.Handlers;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMessageBusClient _messageBus;

    public UpdateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IMessageBusClient messageBus)
    {
        _customerRepository = customerRepository;
        _messageBus = messageBus;
    }

    public async Task<Guid> Handle(
        UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _customerRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Update Customer has error", "Update Customer has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Find Error");
        }

        entity
            .Update(
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

        await _customerRepository.UpdateAsync(entity);

        foreach (var @event in entity.Events)
        {
            var routingKey = @event.GetType().Name.ToDashCase();

            _messageBus.Publish(@event, routingKey, "customer-service");
        }

        return entity.Id;
    }
}