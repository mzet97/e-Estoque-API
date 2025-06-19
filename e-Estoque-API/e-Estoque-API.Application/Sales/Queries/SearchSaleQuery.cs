using e_Estoque_API.Application.Dtos.InputModels;
using e_Estoque_API.Application.Sales.ViewModels;
using e_Estoque_API.Core.Models;
using MediatR;

namespace e_Estoque_API.Application.Sales.Queries;

public class SearchSaleQuery : BaseSearch, IRequest<BaseResultList<SaleViewModel>>
{
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalTax { get; set; }

    public string SaleType { get; set; } = string.Empty;
    public string PaymentType { get; set; } = string.Empty;

    public DateTime DeliveryDate { get; set; }
    public DateTime SaleDate { get; set; }
    public DateTime PaymentDate { get; set; }

    public Guid IdProduct { get; set; }
    public Guid IdCustomer { get; set; }
}