using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Repository.Repositories
{
    public interface IReportRepository : IRepository<UserReport>
    {
        Task<IEnumerable<UserReport>> GetReports(int userId);
    }

    public class ReportRepository : Repository<UserReport>, IReportRepository
    {
        public ReportRepository(IWorkFlowDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async Task<IEnumerable<UserReport>> GetReports(int userId)
        {
            using (IWorkFlowDbContext context = ContextFactory.CreateContext())
            {
                return await context.UserReport
                                    .Where(r => r.UserId == userId)
                                    .ToListAsync();
            }
        }
    }
}
