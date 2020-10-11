using System;
using DCommerce.Data.Shared;

namespace DCommerce.Data.Domain
{
    public class Purchase : BaseEntity
    {
        public string Name { get; set; }

        public Guid ProductId { get; set; } // navigation property
    }
}
