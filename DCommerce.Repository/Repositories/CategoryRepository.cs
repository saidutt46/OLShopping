using System;
using DCommerce.Data.Domain;
using DCommerce.Repository.DatabaseContext;
using DCommerce.Repository.Interfaces;
using DCommerce.Repository.Shared;

namespace DCommerce.Repository.Repositories
{
    public class CategoryRepository : EfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DCommerceContext options) : base(options)
        {

        }
    }
}
