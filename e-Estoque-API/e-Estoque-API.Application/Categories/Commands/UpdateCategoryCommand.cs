using e_Estoque_API.Core.Entities;
using MediatR;

namespace e_Estoque_API.Application.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public UpdateCategoryCommand(Guid id, string name, string shortDescription, string description)
        {
            Id = id;
            Name = name;
            ShortDescription = shortDescription;
            Description = description;
        }

        public Category ToEntity()
        {
            return new Category(Id, Name, Description, ShortDescription);
        }
    }
}
