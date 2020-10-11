using System;
namespace DCommerce.Dto.Responses
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
