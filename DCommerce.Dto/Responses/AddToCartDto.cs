using System;
using System.Collections.Generic;
using DCommerce.Data.Domain;

namespace DCommerce.Dto.Responses
{
    public class AddToCartDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string IdentityId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
