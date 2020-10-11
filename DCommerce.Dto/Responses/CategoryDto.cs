using System;
using System.Collections.Generic;
using DCommerce.Data.Domain;

namespace DCommerce.Dto.Responses
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
