using SSU.ITA.WorkFlow.DataAccess.EF.Entities;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Repository.Repositories
{
    public interface IEmployeeRepository : IRepository<UserInformation>
    {
    }

    public class EmployeeRepository : Repository<UserInformation>, IEmployeeRepository
    {
        public EmployeeRepository(IWorkFlowDbContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
