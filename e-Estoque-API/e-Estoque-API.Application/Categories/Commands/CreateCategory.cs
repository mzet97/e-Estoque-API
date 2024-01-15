using MediatR;

namespace e_Estoque_API.Application.Categories.Commands
{
    public class CreateCategory : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
    }
}
