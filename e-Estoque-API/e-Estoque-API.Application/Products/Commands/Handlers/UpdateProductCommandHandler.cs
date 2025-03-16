using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Products.Commands.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMessageBusClient _messageBus;

    public UpdateProductCommandHandler(
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
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _productRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Update Product has error", "Update Product has error");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Find Error");
        }

        entity.Update(
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

        await _productRepository.UpdateAsync(entity);

        foreach (var @event in entity.Events)
        {
            var routingKey = @event.GetType().Name.ToDashCase();

            _messageBus.Publish(@event, routingKey, "product-service");
        }

        return entity.Id;
    }
}