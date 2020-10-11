using System;
using DCommerce.Data.Domain;
using DCommerce.Repository.DatabaseContext;
using DCommerce.Repository.Interfaces;
using DCommerce.Repository.Shared;

namespace DCommerce.Repository.Repositories
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(DCommerceContext options) : base(options)
        {

        }
    }
}
