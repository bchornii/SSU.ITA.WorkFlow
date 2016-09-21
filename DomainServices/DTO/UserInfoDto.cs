using System.ComponentModel.DataAnnotations;

namespace SSU.ITA.WorkFlow.Domain.Services.DTO
{
    public interface IUserInfoDto
    {
        
    }
    public class UserInfoDto : IUserInfoDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
