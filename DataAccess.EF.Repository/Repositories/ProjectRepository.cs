using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Repository.Repositories
{
    public interface IProjectRepository : IRepository<UserProject>
    {
        Task<IEnumerable<UserProject>> GetProjects(int managerId);
    }

    public class ProjectRepository : Repository<UserProject>, IProjectRepository
    {
        public ProjectRepository(IWorkFlowDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async Task<IEnumerable<UserProject>> GetProjects(int managerId)
        {
            using (IWorkFlowDbContext context = ContextFactory.CreateContext())
            {
                return await context.UserProject
                                    .Where(p => p.CreatorId == managerId)
                                    .ToListAsync();
            }
        }

    }
}
