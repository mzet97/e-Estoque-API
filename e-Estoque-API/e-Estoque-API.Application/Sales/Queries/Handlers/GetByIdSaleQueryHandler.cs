using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Application.Sales.ViewModels;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Sales.Queries.Handlers;

public class GetByIdSaleQueryHandler : IRequestHandler<GetByIdSaleQuery, BaseResult<SaleViewModel>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMessageBusClient _messageBus;

    public GetByIdSaleQueryHandler(
        ISaleRepository saleRepository,
        IMessageBusClient messageBus)
    {
        _saleRepository = saleRepository;
        _messageBus = messageBus;
    }

    public async Task<BaseResult<SaleViewModel>> Handle(
        GetByIdSaleQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _saleRepository.GetByIdAsync(request.Id);

        if (entity is null)
        {
            var noticiation = new NotificationError("Sale not found", "Sale not found");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Not found");
        }

        return new BaseResult<SaleViewModel>(SaleViewModel.FromEntity(entity), true);
    }
}