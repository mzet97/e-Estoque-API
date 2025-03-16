using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Companies.Commands.Handlers;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Unit>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMessageBusClient _messageBus;

    public DeleteCompanyCommandHandler(
        ICompanyRepository companyRepository,
        IMessageBusClient messageBus)
    {
        _companyRepository = companyRepository;
        _messageBus = messageBus;
    }

    public async Task<Unit> Handle(
        DeleteCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _companyRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Delete Company has error", "Delete Company has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Delete Error");
        }

        await _companyRepository.RemoveAsync(entity.Id);

        return Unit.Value;
    }
}