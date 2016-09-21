using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.Domain.Services.DTO;

namespace SSU.ITA.WorkFlow.Application.Web.Controllers
{
    [RoutePrefix("api/reports")]
    public class ReportController : ApiController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [Route("{userId:int}/all")]
        [HttpGet]
        public async Task<IEnumerable<ReportDto>> GetReports(int userId)
        {
            return await _reportService.GetReports(userId);
        }

        [Route("{userId:int}/add")]
        [HttpPost]
        public async Task CreateReport(ReportDto report)
        {
            await _reportService.CreateReport(report);
        }
    }
}