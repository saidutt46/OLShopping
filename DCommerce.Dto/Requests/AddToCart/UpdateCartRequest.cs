using System;
using System.ComponentModel.DataAnnotations;

namespace DCommerce.Dto.Requests.AddToCart
{
    public class UpdateCartRequest
    {
        [Required(ErrorMessage = "Product is required while adding to Cart")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "User Id is required when adding items to Cart")]
        public string IdentityId { get; set; }
        [Required(ErrorMessage = "Quantity is required when adding items to Cart")]
        public int Quantity { get; set; }
        public Guid Id { get; set; }
    }
}
