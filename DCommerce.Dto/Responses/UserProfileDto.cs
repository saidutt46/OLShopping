using System;
namespace DCommerce.Dto.Responses
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public int ColorTheme { get; set; }
    }
}
