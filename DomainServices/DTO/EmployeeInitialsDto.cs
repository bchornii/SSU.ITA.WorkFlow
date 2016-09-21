namespace SSU.ITA.WorkFlow.Domain.Services.DTO
{
    public interface IEmployeeDto
    {
    }

    public class EmployeeInitialsDto : IEmployeeDto
    {        
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}
