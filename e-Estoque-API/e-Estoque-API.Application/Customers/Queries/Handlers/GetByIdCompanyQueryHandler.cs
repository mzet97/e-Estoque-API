using e_Estoque_API.Application.Customers.ViewModels;
using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Customers.Queries.Handlers
{
    public class GetByICustomerQueryHandler : IRequestHandler<GetByIdCustomerQuery, CustomerViewModel>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMessageBusClient _messageBus;

        public GetByICustomerQueryHandler(ICustomerRepository customerRepository, IMessageBusClient messageBus)
        {
            _customerRepository = customerRepository;
            _messageBus = messageBus;
        }

        public async Task<CustomerViewModel> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
        {
            var entity = await _customerRepository.GetById(request.Id);

            if (entity == null)
            {
                var noticiation = new NotificationError("Not found Customer", "Not found Customer");
                var routingKey = noticiation.GetType().Name.ToDashCase();

                _messageBus.Publish(noticiation, routingKey, "noticiation-service");

                throw new NotFoundException("Not found");
            }

            return CustomerViewModel.FromEntity(entity);
        }
    }
}
