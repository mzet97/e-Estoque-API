﻿using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.Context;

namespace e_Estoque_API.Infrastructure.Repositories
{
    public class SaleProductRepository : Repository<SaleProduct>, ISaleProductRepository
    {
        public SaleProductRepository(DataIdentityDbContext db) : base(db)
        {
        }
    }
}