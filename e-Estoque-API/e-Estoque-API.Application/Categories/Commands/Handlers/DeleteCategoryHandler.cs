using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Categories.Commands.Handlers;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly ICategoryRepository _categoryRepository;

    private readonly IMessageBusClient _messageBus;

    public DeleteCategoryHandler(
        ICategoryRepository categoryRepository,
        IMessageBusClient messageBus)
    {
        _categoryRepository = categoryRepository;
        _messageBus = messageBus;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _categoryRepository.GetById(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Delete Category has error", "Delete Category has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Delete Error");
        }

        await _categoryRepository.Remove(entity.Id);

        return Unit.Value;
    }
}