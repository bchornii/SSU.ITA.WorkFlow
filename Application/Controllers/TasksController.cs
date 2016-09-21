using SSU.ITA.WorkFlow.Application.Web.Filters;
using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSU.ITA.WorkFlow.Application.Web.Controllers
{
    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        private ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("{taskId:int}")]
        public async Task<ITaskDto> GetTaskInformation(int taskId)
        {
            return await _taskService.FetchEmployeeTask(taskId);            
        }

        [HttpGet]
        [Route("statuses")]
        public async Task<IReadOnlyList<WorkItemStatusDto>> GetTaskStatuses()
        {
            return await _taskService.FetchTaskStatuses();            
        }

        [HttpPost]
        [ValidateBindingModel]
        [Route("update")]
        public async Task<IHttpActionResult> UpdateTaskInformation(TaskDto taskInformation)
        {
            await _taskService.CreateOrUpdateTaskInformation(taskInformation);
            return Ok();
        }
    }
}
