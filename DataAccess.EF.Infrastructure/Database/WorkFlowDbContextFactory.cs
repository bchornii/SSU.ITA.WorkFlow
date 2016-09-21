namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database
{
    public interface IWorkFlowDbContextFactory
    {
        IWorkFlowDbContext CreateContext();        
    }

    public class WorkFlowDbContextFactory : IWorkFlowDbContextFactory
    {
        public IWorkFlowDbContext CreateContext()
        {
            return new WorkFlowDbContext();
        }        
    }
}