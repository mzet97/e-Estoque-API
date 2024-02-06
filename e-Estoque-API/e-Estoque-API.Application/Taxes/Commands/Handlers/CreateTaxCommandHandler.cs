using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Taxes.Commands.Handlers
{
    public class CreateTaxCommandHandler : IRequestHandler<CreateTaxCommand, Guid>
    {
        private readonly ITaxRepository _taxRepository;
        private readonly IMessageBusClient _messageBus;

        public CreateTaxCommandHandler(ITaxRepository taxRepository, IMessageBusClient messageBus)
        {
            _taxRepository = taxRepository;
            _messageBus = messageBus;
        }

        public async Task<Guid> Handle(CreateTaxCommand request, CancellationToken cancellationToken)
        {
            var entity = Tax.Create(request.Name, request.Description, request.Percentage, request.IdCategory);

            if (!Validator.Validate(new TaxValidation(), entity))
            {
                var noticiation = new NotificationError("Validate Tax has error", "Validate Tax has error");
                var routingKey = noticiation.GetType().Name.ToDashCase();

                _messageBus.Publish(noticiation, routingKey, "noticiation-service");

                throw new ValidationException("Validate Error");
            }

            await _taxRepository.Add(entity);

            foreach (var @event in entity.Events)
            {
                var routingKey = @event.GetType().Name.ToDashCase();

                _messageBus.Publish(@event, routingKey, "tax-service");
            }

            return entity.Id;
        }
    }
}
