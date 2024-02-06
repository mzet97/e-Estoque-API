using MediatR;

namespace e_Estoque_API.Application.Companies.Commands
{
    public class DeleteCompanyCommand : IRequest<Unit>
    {
        public DeleteCompanyCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

     
    }
}
