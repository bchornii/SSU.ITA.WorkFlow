namespace SSU.ITA.WorkFlow.Domain.Services.DTO
{
    public class TaskDescriptionDto : TaskNameDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }     
        public int StatusId { get; set; }   
        public string StatusName { get; set; }
        public string Description { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeSecondName { get; set; }        
    }
}
