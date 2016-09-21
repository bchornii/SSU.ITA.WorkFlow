using System.ComponentModel.DataAnnotations;

namespace SSU.ITA.WorkFlow.Domain.Services.DTO
{
    public interface ITaskDto
    {

    }
    public class TaskNameDto : ITaskDto
    {
        [Required]
        public int TaskId { get; set; }    
        [Required]    
        public string TaskName { get; set; }        
    }
}
