using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Sales.Commands.Handlers;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMessageBusClient _messageBus;

    public CreateSaleCommandHandler(
        ISaleRepository saleRepository,
        ICompanyRepository companyRepository,
        IMessageBusClient messageBus,
        IProductRepository productRepository)
    {
        _saleRepository = saleRepository;
        _companyRepository = companyRepository;
        _messageBus = messageBus;
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(request.IdCustomer);

        if (company == null)
        {
            var noticiation = new NotificationError("Company not found", "Company not found");
            var routingKey = noticiation.GetType().Name.ToDashCase();
            _messageBus.Publish(noticiation, routingKey, "noticiation-service");
            throw new NotFoundException("Company not found");
        }

        var customer = await _companyRepository.GetByIdAsync(request.IdCustomer);

        if (customer == null)
        {
            var noticiation = new NotificationError("Customer not found", "Customer not found");
            var routingKey = noticiation.GetType().Name.ToDashCase();
            _messageBus.Publish(noticiation, routingKey, "noticiation-service");
            throw new NotFoundException("Customer not found");
        }

        var products = new List<Product>();
        foreach(var idProduct in request.IdsProducts)
        {
            var product = await _productRepository.GetByIdAsync(idProduct);
            if (product == null)
            {
                var noticiation = new NotificationError("Product not found", "Product not found");
                var routingKey = noticiation.GetType().Name.ToDashCase();
                _messageBus.Publish(noticiation, routingKey, "noticiation-service");
                throw new NotFoundException("Product not found");
            }

            products.Add(product);
        }


        var entity = Sale.Create(
            request.Quantity,
            request.TotalPrice,
            request.TotalTax,
            request.SaleType,
            request.PaymentType,
            request.DeliveryDate,
            request.SaleDate,
            request.PaymentDate,
            request.IdCustomer,
            request.IdsProducts.Select(x => new SaleProduct(request.Quantity, x)).ToList()
            );

        if (!entity.IsValid())
        {
            var errors = String.Join(",", entity.GetErrors());
            var noticiation = new NotificationError("Validate Sales has error", errors);
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new ValidationException(errors);
        }

        await _saleRepository.AddAsync(entity);

        foreach (var @event in entity.Events)
        {
            var routingKey = @event.GetType().Name.ToDashCase();
            _messageBus.Publish(@event, routingKey, "sale-service");
        }

        return entity.Id;
    }
}
