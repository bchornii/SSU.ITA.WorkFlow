using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.Domain.Services.DTO;

namespace SSU.ITA.WorkFlow.Application.Web.Controllers
{
    [RoutePrefix("api/roles")]
    public class UserRolesController : ApiController
    {
        private IUserRolesService _service;

        public UserRolesController(IUserRolesService service)
        {
            _service = service;
        }

        public async Task<List<string>> GetRoles()
        {
            var roles = await _service.GetUserRoles();
            roles.Remove("Admin");
            roles.Remove("Anonymous");
            return roles;
        }
        [Route("get")]
        [HttpGet]
        public async Task<IReadOnlyList<IRoleDto>> GetRolesFull()
        {
            return await _service.GetUserRolesFull();
        }
    }
}
