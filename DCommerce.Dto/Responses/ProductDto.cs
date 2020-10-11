using System;
using DCommerce.Data.Domain;
using DCommerce.Data.Helpers;

namespace DCommerce.Dto.Responses
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short QuantityInPackage { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string ImagePath { get; set; }
        public double? UnitPrice { get; set; }

        public Guid CategoryId { get; set; } // navigation property
    }
}
