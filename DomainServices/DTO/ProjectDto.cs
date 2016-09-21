using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSU.ITA.WorkFlow.Domain.Services.DTO
{
    public class ProjectDto : IProjectDto
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CreatorId { get; set; }
        public IEnumerable<EmployeeInitialsDto> Employees { get; set; }
    }
}