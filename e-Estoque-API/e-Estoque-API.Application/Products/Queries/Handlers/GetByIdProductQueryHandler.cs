using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Application.Products.ViewModels;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Products.Queries.Handlers;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, BaseResult<ProductViewModel>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMessageBusClient _messageBus;

    public GetByIdProductQueryHandler(
        IProductRepository productRepository,
        IMessageBusClient messageBus)
    {
        _productRepository = productRepository;
        _messageBus = messageBus;
    }

    public async Task<BaseResult<ProductViewModel>> Handle(
        GetByIdProductQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _productRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Not found Product", "Not found Product");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Not found");
        }

        return new BaseResult<ProductViewModel>(ProductViewModel.FromEntity(entity), true);
    }
}