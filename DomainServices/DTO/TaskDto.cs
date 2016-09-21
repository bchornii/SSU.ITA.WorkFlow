using System.ComponentModel.DataAnnotations;

namespace SSU.ITA.WorkFlow.Domain.Services.DTO
{
    public class TaskDto : TaskNameDto
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int StatusId { get; set; }        
        public string Description { get; set; }
    }
}
