using System;
using DCommerce.Data.Domain;
using DCommerce.Repository.Helpers;

namespace DCommerce.Repository.ApplicationSpecifications
{
    public class ItemsInCartForUserSpecification : BaseSpecification<CartItem>
    {
        public ItemsInCartForUserSpecification(string IdentityId) : base(x => x.IdentityId == IdentityId)
        {
            AddInclude(x => x.Product);
        }
    }
}
