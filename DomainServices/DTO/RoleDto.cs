namespace SSU.ITA.WorkFlow.Domain.Services.DTO
{
    public interface IRoleDto
    {

    }
    class RoleDto : IRoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
