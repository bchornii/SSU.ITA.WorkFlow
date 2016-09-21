using System;
using System.Collections.Generic;

namespace SSU.ITA.WorkFlow.Domain.Services.DTO
{
    public class ProjectWithTasksDto : IProjectDto
    {
        public int ProjectId { get; set; }        
        public string ProjectName { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<ITaskDto> Tasks { get; set; }
    }
}
