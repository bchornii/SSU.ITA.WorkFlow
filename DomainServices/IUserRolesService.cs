using SSU.ITA.WorkFlow.Domain.Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSU.ITA.WorkFlow.Domain.Services
{
    public interface IUserRolesService
    {
        Task<List<string>> GetUserRoles();
        Task<IReadOnlyList<IRoleDto>> GetUserRolesFull();
    }
}
