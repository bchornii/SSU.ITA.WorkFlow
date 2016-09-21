using SSU.ITA.WorkFlow.Application.Web.Filters;
using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSU.ITA.WorkFlow.Application.Web.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
        private IEmployeeService _userService;
        public EmployeesController(IEmployeeService userService)
        {
            _userService = userService;
        }

        [Route("{managerId:int}/all")]
        [HttpGet]
        public async Task<IEnumerable<IEmployeeDto>> GetEmployeesNames(int managerId)
        {            
            return await _userService.FetchEmployeeNames(managerId);            
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IUserInfoDto> GetEmployeeInformation(int id)
        {
            return await _userService.FetchEmployeeInfo(id);
        }

        [Route("update")]
        [ValidateBindingModel]
        [HttpPost]
        public async Task UpdateEmployeeInformation(UserInfoDto user)
        {
            await _userService.UpdateEmployeeInfo(user);
        }

        [Route("{empId:int}/projects")]
        [HttpGet]
        public async Task<IEnumerable<IProjectDto>> GetEmployeeProjects(int empId)
        {
            return await _userService.FetchEmployeeProjectsList(empId);            
        }

        [Route("{empId:int}/projects/{pageNum:int?}/{numPerPage:int?}")]
        [HttpGet]
        public async Task<IPagerListDto> GetEmployeeProjectPage(int empId, int pageNum = 1, int numPerPage = 10)
        {
            return await _userService.FetchEmployeeProjects(empId, pageNum, numPerPage);            
        }
    }
}
