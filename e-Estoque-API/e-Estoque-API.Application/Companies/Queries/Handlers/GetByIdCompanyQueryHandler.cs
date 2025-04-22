using e_Estoque_API.Application.Companies.ViewModels;
using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Companies.Queries.Handlers;

public class GetByIdCompanyQueryHandler : IRequestHandler<GetByIdCompanyQuery, BaseResult<CompanyViewModel>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMessageBusClient _messageBus;

    public GetByIdCompanyQueryHandler(
        ICompanyRepository companyRepository,
        IMessageBusClient messageBus)
    {
        _companyRepository = companyRepository;
        _messageBus = messageBus;
    }

    public async Task<BaseResult<CompanyViewModel>> Handle(
        GetByIdCompanyQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _companyRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var noticiation = new NotificationError("Not found Company", "Not found Company");
            var routingKey = noticiation.GetType().Name.ToDashCase();

            _messageBus.Publish(noticiation, routingKey, "noticiation-service");

            throw new NotFoundException("Not found");
        }

        return new BaseResult<CompanyViewModel>(CompanyViewModel.FromEntity(entity), true);
    }
}