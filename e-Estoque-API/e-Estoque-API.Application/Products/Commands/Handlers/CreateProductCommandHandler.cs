using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Application.Products.Commands;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Taxes.Commands.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMessageBusClient _messageBus;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IMessageBusClient messageBus,
        ICategoryRepository categoryRepository,
        ICompanyRepository companyRepository)
    {
        _productRepository = productRepository;
        _messageBus = messageBus;
        _categoryRepository = categoryRepository;
        _companyRepository = companyRepository;
    }

    public async Task<Guid> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var entity = Product.Create(
            request.Name,
            request.Description,
            request.ShortDescription,
            request.Price,
            request.Weight,
            request.Height,
            request.Length,
            request.Image,
            request.IdCategory,
            request.IdCompany);

        if (!entity.IsValid())
        {
            var noticiation = new NotificationError("Validate Product has error", "Validate Product has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new ValidationException("Validate Error");
        }

        var category = await _categoryRepository.GetByIdAsync(request.IdCategory);

        if (category == null)
        {
            var noticiation = new NotificationError("Category not found", "Category not found");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Category not found");
        }

        var company = await _companyRepository.GetByIdAsync(request.IdCompany);

        if (company == null)
        {
            var noticiation = new NotificationError("Company not found", "Company not found");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Company not found");
        }

        await _productRepository.AddAsync(entity);

        foreach (var @event in entity.Events)
        {
            var routingKey = @event.GetType().Name.ToDashCase();

            _messageBus.Publish(@event, routingKey, "product-service");
        }

        return entity.Id;
    }
}