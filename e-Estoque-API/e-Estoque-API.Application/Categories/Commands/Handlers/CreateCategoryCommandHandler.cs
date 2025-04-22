using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Categories.Commands.Handlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMessageBusClient _messageBus;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository,
                                        IMessageBusClient messageBus)
    {
        _categoryRepository = categoryRepository;
        _messageBus = messageBus;
    }

    public async Task<Guid> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var entity = Category.Create(
            request.Name,
            request.Description,
            request.ShortDescription);

        if (!entity.IsValid())
        {
            var errors = String.Join(",", entity.GetErrors());
            var noticiation = new NotificationError("Validate Category has error", errors);
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new ValidationException(errors);
        }

        await _categoryRepository.AddAsync(entity);

        foreach (var @event in entity.Events)
        {
            var routingKey = @event.GetType().Name.ToDashCase();

            _messageBus.Publish(@event, routingKey, "category-service");
        }

        return entity.Id;
    }
}