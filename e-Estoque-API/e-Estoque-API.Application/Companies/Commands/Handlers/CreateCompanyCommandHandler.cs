using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Companies.Commands.Handlers
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMessageBusClient _messageBus;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMessageBusClient messageBus)
        {
            _companyRepository = companyRepository;
            _messageBus = messageBus;
        }

        public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = Company.Create(
                request.Name,
                request.DocId,
                request.Email,
                request.Description,
                request.PhoneNumber,
                new CompanyAddress(
                    request.Address.Street,
                    request.Address.Number,
                    request.Address.Complement,
                    request.Address.Neighborhood,
                    request.Address.District,
                    request.Address.City,
                    request.Address.County,
                    request.Address.ZipCode,
                    request.Address.Latitude,
                    request.Address.Longitude));

            if (!Validator.Validate(new CompanyValidation(), entity))
            {
                var noticiation = new NotificationError("Validate Company has error", "Validate Company has error");
                var routingKey = noticiation.GetType().Name.ToDashCase();

                _messageBus.Publish(noticiation, routingKey, "noticiation-service");

                throw new ValidationException("Validate Error");
            }

            if (!Validator.Validate(new CompanyAddressValidation(), entity.CompanyAddress))
            {
                var noticiation = new NotificationError("Validate Address has error", "Validate Address has error");
                var routingKey = noticiation.GetType().Name.ToDashCase();

                _messageBus.Publish(noticiation, routingKey, "noticiation-service");

                throw new ValidationException("Validate Error");
            }

            await _companyRepository.Add(entity);

            foreach (var @event in entity.Events)
            {
                var routingKey = @event.GetType().Name.ToDashCase();

                _messageBus.Publish(@event, routingKey, "company-service");
            }

            return entity.Id;
        }
    }
}
