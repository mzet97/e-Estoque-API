﻿using e_Estoque_API.Application.Customers.ViewModels;
using e_Estoque_API.Application.Dtos.InputModels;
using e_Estoque_API.Core.Models;
using MediatR;

namespace e_Estoque_API.Application.Customers.Queries;

public class SearchCustomerQuery : BaseSearch, IRequest<BaseResultList<CustomerViewModel>>
{
    public string Name { get; set; } = string.Empty;
    public string DocId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}