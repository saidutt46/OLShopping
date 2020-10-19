using System;
using System.ComponentModel.DataAnnotations;

namespace DCommerce.Dto.Requests.Product
{
    public class ProductUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityInPackage { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double UnitPrice { get; set; }

        public virtual Guid CategoryId { get; set; }
    }
}
