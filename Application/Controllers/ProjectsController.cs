using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSU.ITA.WorkFlow.Application.Web.Controllers
{
    [RoutePrefix("api/projects")]
    public class ProjectsController : ApiController
    {
        private readonly IProjectsService _projectService;

        public ProjectsController(IProjectsService projectService)
        {
            _projectService = projectService;
        }

        [Route("{managerId:int}/all")]
        [HttpGet]
        public async Task<IEnumerable<ProjectNameDto>> GetProjects(int managerId)
        {
            return await _projectService.GetProjects(managerId);
        }

        [Route("create")]
        [HttpPost]
        public async Task<int> CreateNewProject(ProjectDto project)
        {
            return await _projectService.CreateNewProject(project);
        }

        [HttpPost]
        [Route("{managerId:int}/add")]
        public async Task<IEnumerable<EmployeeInitialsDto>> GetEmployees(int managerId, int[] EmployeesId)
        {
            return await _projectService.GetEmployees(managerId, EmployeesId);
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<ProjectDto> GetProject(int id)
        {
            return await _projectService.GetProject(id);
        }

        [Route("update")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateProject(ProjectDto project)
        {
            await _projectService.UpdateProject(project);
            return Ok();
        }
    }
}