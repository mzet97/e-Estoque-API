using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Application.Taxes.ViewModels;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Taxes.Queries.Handlers;

public class GetByIdTaxQueryHandler : IRequestHandler<GetByIdTaxQuery, BaseResult<TaxViewModel>>
{
    private readonly ITaxRepository _taxRepository;
    private readonly IMessageBusClient _messageBus;

    public GetByIdTaxQueryHandler(
        ITaxRepository taxRepository,
        IMessageBusClient messageBus)
    {
        _taxRepository = taxRepository;
        _messageBus = messageBus;
    }

    public async Task<BaseResult<TaxViewModel>> Handle(
        GetByIdTaxQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _taxRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Not found Tax", "Not found Tax");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Not found");
        }

        return new BaseResult<TaxViewModel>(TaxViewModel.FromEntity(entity), true);
    }
}