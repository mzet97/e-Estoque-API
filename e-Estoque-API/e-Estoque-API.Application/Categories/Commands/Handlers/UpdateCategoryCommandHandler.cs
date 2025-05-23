﻿using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Categories.Commands.Handlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMessageBusClient _messageBus;

    public UpdateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IMessageBusClient messageBus)
    {
        _categoryRepository = categoryRepository;
        _messageBus = messageBus;
    }

    public async Task<Guid> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _categoryRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Update Category has error", "Update Category has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Find Error");
        }

        entity
           .Update(
            request.Name,
            request.ShortDescription,
            request.Description);

        if (!entity.IsValid())
        {
            var errors = String.Join(",", entity.GetErrors());
            var noticiation = new NotificationError("Validate Category has error", errors);
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new ValidationException(errors);
        }

        await _categoryRepository.UpdateAsync(entity);

        foreach (var @event in entity.Events)
        {
            var routingKey = @event.GetType().Name.ToDashCase();

            _messageBus.Publish(@event, routingKey, "category-service");
        }

        return entity.Id;
    }
}