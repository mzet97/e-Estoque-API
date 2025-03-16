using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Companies.Commands.Handlers;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Guid>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMessageBusClient _messageBus;

    public UpdateCompanyCommandHandler(
        ICompanyRepository companyRepository,
        IMessageBusClient messageBus)
    {
        _companyRepository = companyRepository;
        _messageBus = messageBus;
    }

    public async Task<Guid> Handle(
        UpdateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _companyRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Update Company has error", "Update Company has error");
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
                request.CompanyAddress);

        if (!entity.IsValid())
        {
            var noticiation = new NotificationError("Validate Company has error", "Validate Company has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new ValidationException("Validate Error");
        }

        await _companyRepository.UpdateAsync(entity);

        foreach (var @event in entity.Events)
        {
            var routingKey = @event.GetType().Name.ToDashCase();

            _messageBus.Publish(@event, routingKey, "category-service");
        }

        return entity.Id;
    }
}