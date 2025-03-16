using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Companies.Commands.Handlers;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Guid>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMessageBusClient _messageBus;

    public CreateCompanyCommandHandler(
        ICompanyRepository companyRepository,
        IMessageBusClient messageBus)
    {
        _companyRepository = companyRepository;
        _messageBus = messageBus;
    }

    public async Task<Guid> Handle(
        CreateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var entity = Company.Create(
            request.Name,
            request.DocId,
            request.Email,
            request.Description,
            request.PhoneNumber,
            request.CompanyAddress);

        if (!entity.IsValid())
        {
            var noticiation = new NotificationError("Validate Company has error", "Validate Company has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new ValidationException("Validate Error");
        }

        await _companyRepository.AddAsync(entity);

        foreach (var @event in entity.Events)
        {
            var routingKey = @event.GetType().Name.ToDashCase();

            _messageBus.Publish(@event, routingKey, "company-service");
        }

        return entity.Id;
    }
}