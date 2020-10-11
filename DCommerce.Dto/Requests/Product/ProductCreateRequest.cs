using System;
using System.ComponentModel.DataAnnotations;
using DCommerce.Data.Helpers;

namespace DCommerce.Dto.Requests.Product
{
    public class ProductCreateRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public short QuantityInPackage { get; set; }
        [Required]
        public string UnitOfMeasurement { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public double? UnitPrice { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
