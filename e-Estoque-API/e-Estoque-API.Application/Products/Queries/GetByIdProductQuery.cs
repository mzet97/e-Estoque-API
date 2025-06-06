﻿using e_Estoque_API.Application.Products.ViewModels;
using e_Estoque_API.Core.Models;
using MediatR;

namespace e_Estoque_API.Application.Products.Queries;

public class GetByIdProductQuery : IRequest<BaseResult<ProductViewModel>>
{
    public Guid Id { get; set; }

    public GetByIdProductQuery(Guid id)
    {
        Id = id;
    }
}