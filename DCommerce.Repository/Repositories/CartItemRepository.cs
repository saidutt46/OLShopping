using System;
using DCommerce.Data.Domain;
using DCommerce.Repository.DatabaseContext;
using DCommerce.Repository.Interfaces;
using DCommerce.Repository.Shared;

namespace DCommerce.Repository.Repositories
{
    public class CartItemRepository : EfRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(DCommerceContext options) : base(options)
        {
        }
    }
}
