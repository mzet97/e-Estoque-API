using e_Estoque_API.Application.Dtos.InputModels;
using e_Estoque_API.Application.Taxes.ViewModels;
using e_Estoque_API.Core.Models;
using MediatR;

namespace e_Estoque_API.Application.Taxes.Queries;

public class SearchTaxQuery : BaseSearch, IRequest<BaseResultList<TaxViewModel>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Percentage { get; set; }

    public Guid IdCategory { get; set; }
}