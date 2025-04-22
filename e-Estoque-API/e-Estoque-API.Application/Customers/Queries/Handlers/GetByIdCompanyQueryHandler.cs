using e_Estoque_API.Application.Customers.ViewModels;
using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Customers.Queries.Handlers;

public class GetByICustomerQueryHandler : IRequestHandler<GetByIdCustomerQuery, BaseResult<CustomerViewModel>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMessageBusClient _messageBus;

    public GetByICustomerQueryHandler(
        ICustomerRepository customerRepository,
        IMessageBusClient messageBus)
    {
        _customerRepository = customerRepository;
        _messageBus = messageBus;
    }

    public async Task<BaseResult<CustomerViewModel>> Handle(
        GetByIdCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _customerRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Not found Customer", "Not found Customer");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Not found");
        }

        return new BaseResult<CustomerViewModel>(CustomerViewModel.FromEntity(entity), true);
    }
}