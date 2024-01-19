using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Categories.Commands.Handlers
{
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

        public async Task<Guid> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _categoryRepository.GetById(request.Id);

            if (entity == null)
            {
                var noticiation = new NotificationError("Update Category has error", "Update Category has error");
                var routingKey = noticiation.GetType().Name.ToDashCase();

                _messageBus.Publish(noticiation, routingKey, "noticiation-service");

                throw new NotFoundException("Find Error");
            }

            if (!Validator.Validate(new CategoryValidation(), entity))
            {
                var noticiation = new NotificationError("Validate CategoryRestaurant has error", "Validate CategoryRestaurant has error");
                var routingKey = noticiation.GetType().Name.ToDashCase();

                _messageBus.Publish(noticiation, routingKey, "noticiation-service");

                throw new ValidationException("Validate Error");
            }

            entity
                .Update(request.Name, request.ShortDescription, request.Description);

            await _categoryRepository.Update(entity);

            foreach (var @event in entity.Events)
            {
                var routingKey = @event.GetType().Name.ToDashCase();

                _messageBus.Publish(@event, routingKey, "category-service");
            }

            return entity.Id;
        }
    }
}
