using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DCommerce.Data.Domain;

namespace DCommerce.Dto.Requests.Category
{
    public class CategoryCreateRequest
    {
        [Required(ErrorMessage = "Category Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
