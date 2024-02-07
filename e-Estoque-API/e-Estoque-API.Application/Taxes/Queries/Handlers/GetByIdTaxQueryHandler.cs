using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Application.Taxes.ViewModels;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Taxes.Queries.Handlers;

public class GetByIdTaxQueryHandler : IRequestHandler<GetByIdTaxQuery, TaxViewModel>
{
    private readonly ITaxRepository _taxRepository;
    private readonly IMessageBusClient _messageBus;

    public GetByIdTaxQueryHandler(ITaxRepository taxRepository, IMessageBusClient messageBus)
    {
        _taxRepository = taxRepository;
        _messageBus = messageBus;
    }

    public async Task<TaxViewModel> Handle(GetByIdTaxQuery request, CancellationToken cancellationToken)
    {
        var entity = await _taxRepository.GetById(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Not found Tax", "Not found Tax");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Not found");
        }

        return TaxViewModel.FromEntity(entity);
    }
}