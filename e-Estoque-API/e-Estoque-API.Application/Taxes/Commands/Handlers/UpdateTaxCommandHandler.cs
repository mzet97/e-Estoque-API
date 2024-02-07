using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Taxes.Commands.Handlers
{
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

        public async Task<Guid> Handle(UpdateTaxCommand request, CancellationToken cancellationToken)
        {
            var entity = await _taxRepository.GetById(request.Id);

            if (entity == null)
            {
                var noticiation = new NotificationError("Update Tax has error", "Update Tax has error");
                var routingKey = noticiation.GetType().Name.ToDashCase();

                _messageBus.Publish(noticiation, routingKey, "noticiation-service");

                throw new NotFoundException("Find Error");
            }

            entity
                .Update(request.Name, request.Description, request.Percentage, request.IdCategory);

            if (!Validator.Validate(new TaxValidation(), entity))
            {
                var noticiation = new NotificationError("Validate Tax has error", "Validate Tax has error");
                var routingKey = noticiation.GetType().Name.ToDashCase();

                _messageBus.Publish(noticiation, routingKey, "noticiation-service");

                throw new ValidationException("Validate Error");
            }

            var category = await _categoryRepository.GetById(request.IdCategory);

            if (category == null)
            {
                var noticiation = new NotificationError("Category not found", "Category not found");
                var routingKey = noticiation.GetType().Name.ToDashCase();

                _messageBus.Publish(noticiation, routingKey, "noticiation-service");

                throw new ValidationException("Category not found");
            }

            await _taxRepository.Update(entity);

            foreach (var @event in entity.Events)
            {
                var routingKey = @event.GetType().Name.ToDashCase();

                _messageBus.Publish(@event, routingKey, "tax-service");
            }

            return entity.Id;
        }
    }
}