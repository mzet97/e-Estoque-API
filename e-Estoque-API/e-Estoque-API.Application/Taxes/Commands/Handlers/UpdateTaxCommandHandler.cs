using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Taxes.Commands.Handlers;

public class UpdateTaxCommandHandler : IRequestHandler<UpdateTaxCommand, Guid>
{
    private readonly ITaxRepository _taxRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMessageBusClient _messageBus;

    public UpdateTaxCommandHandler(
        ITaxRepository taxRepository,
        IMessageBusClient messageBus,
        ICategoryRepository categoryRepository)
    {
        _taxRepository = taxRepository;
        _messageBus = messageBus;
        _categoryRepository = categoryRepository;
    }

    public async Task<Guid> Handle(
        UpdateTaxCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _taxRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var errors = String.Join(",", entity.GetErrors());
            var noticiation = new NotificationError("Update Tax has error", errors);
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException(errors);
        }

        var category = await _categoryRepository.GetByIdAsync(request.IdCategory);

        if (category == null)
        {
            var noticiation = new NotificationError("Category not found", "Category not found");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new ValidationException("Category not found");
        }

        entity
            .Update(
            request.Name,
            request.Description,
            request.Percentage,
            request.IdCategory,
            category);

        if (!entity.IsValid())
        {
            var errors = String.Join("", entity.GetErrors());
            var noticiation = new NotificationError("Validate Tax has error", errors);
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new ValidationException(errors);
        }

        

        await _taxRepository.UpdateAsync(entity);

        foreach (var @event in entity.Events)
        {
            var routingKey = @event.GetType().Name.ToDashCase();

            _messageBus.Publish(@event, routingKey, "tax-service");
        }

        return entity.Id;
    }
}