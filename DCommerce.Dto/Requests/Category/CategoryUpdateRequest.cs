using System;
using System.ComponentModel.DataAnnotations;

namespace DCommerce.Dto.Requests.Category
{
    public class CategoryUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
