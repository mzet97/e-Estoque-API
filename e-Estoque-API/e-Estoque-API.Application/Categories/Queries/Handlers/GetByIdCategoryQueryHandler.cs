using e_Estoque_API.Application.Categories.ViewModels;
using e_Estoque_API.Application.Extensions;
using e_Estoque_API.Core.Events;
using e_Estoque_API.Core.Exceptions;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.MessageBus;
using MediatR;

namespace e_Estoque_API.Application.Categories.Queries.Handlers
{
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, CategoryViewModel>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMessageBusClient _messageBus;

        public GetByIdCategoryQueryHandler(ICategoryRepository categoryRepository, IMessageBusClient messageBus)
        {
            _categoryRepository = categoryRepository;
            _messageBus = messageBus;
        }

        public async Task<CategoryViewModel> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var entity = await _categoryRepository.GetById(request.Id);

            if (entity == null)
            {
                var noticiation = new NotificationError("Not found Category", "Not found Category");
                var routingKey = noticiation.GetType().Name.ToDashCase();

                _messageBus.Publish(noticiation, routingKey, "noticiation-service");

                throw new NotFoundException("Not found");
            }

            return CategoryViewModel.FromEntity(entity);
        }
    }
}
