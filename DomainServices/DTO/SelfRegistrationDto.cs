using System.ComponentModel.DataAnnotations;

namespace SSU.ITA.WorkFlow.Domain.Services.DTO
{
    public class SelfRegistrationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RegistrationToken { get; set; }
    }
}