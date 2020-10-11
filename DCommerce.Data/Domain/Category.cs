using System;
using System.Collections.Generic;
using DCommerce.Data.Shared;

namespace DCommerce.Data.Domain
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
