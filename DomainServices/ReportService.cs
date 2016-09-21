using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;
using SSU.ITA.WorkFlow.DataAccess.EF.Repository.Repositories;
using SSU.ITA.WorkFlow.Domain.Services.DTO;

namespace SSU.ITA.WorkFlow.Domain.Services
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDto>> GetReports(int userId);
        Task CreateReport(ReportDto report);
    }

    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<IEnumerable<ReportDto>> GetReports(int userId)
        {
            IEnumerable<UserReport> reports = await _reportRepository.GetReports(userId);

            return reports.Select(r => new ReportDto
            {
                ReportId = r.ReportId,
                UserId = r.UserId,
                TaskId = r.TaskId,
                CreateDate = r.CreateDate,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                Comment = r.Comment
            });
        }

        public async Task CreateReport(ReportDto report)
        {
            var reportEntity = new UserReport
            {
                UserId = report.UserId,
                TaskId = report.TaskId,
                CreateDate = report.CreateDate,
                StartTime = report.StartTime,
                EndTime = report.EndTime,
                Comment = report.Comment
            };

            await _reportRepository.Save(reportEntity);
        }
    }
}
