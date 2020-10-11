using System;
using System.ComponentModel.DataAnnotations;
using DCommerce.Data.Helpers;
using DCommerce.Data.Shared;

namespace DCommerce.Data.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public short QuantityInPackage { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }
        public string ImagePath { get; set; }
        public double? UnitPrice { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
