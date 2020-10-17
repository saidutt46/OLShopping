using System;
using System.ComponentModel.DataAnnotations;
using DCommerce.Data.Shared;

namespace DCommerce.Data.Domain
{
    public class CartItem : BaseEntity
    {
        [Required]
        public int Quantity { get; set; }

        // navigation properties
        [Required]
        public string IdentityId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
