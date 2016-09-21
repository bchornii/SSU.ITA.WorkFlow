using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Save(TEntity entity);
        Task SaveMany(IEnumerable<TEntity> entities);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IWorkFlowDbContextFactory ContextFactory;
        protected Repository(IWorkFlowDbContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task Save(TEntity entity)
        {
            using (IWorkFlowDbContext context = ContextFactory.CreateContext())
            {
                context.Set<TEntity>().Add(entity);

                await context.SaveChangesAsync();
            }
        }

        public async Task SaveMany(IEnumerable<TEntity> entities)
        {
            using (IWorkFlowDbContext context = ContextFactory.CreateContext())
            {
                context.Set<TEntity>().AddRange(entities);

                await context.SaveChangesAsync();
            }
        }
    }
}
