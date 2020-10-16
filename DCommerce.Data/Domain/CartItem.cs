using System;
using DCommerce.Data.Shared;

namespace DCommerce.Data.Domain
{
    public class CartItem : BaseEntity
    {
        public int Quantity { get; set; }

        // navigation properties
        public string IdentityId { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
