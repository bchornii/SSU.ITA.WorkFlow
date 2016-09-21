using System;
using System.Data.Entity;
using System.Threading.Tasks;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Configurations;
using TaskStatus = SSU.ITA.WorkFlow.DataAccess.EF.Entities.TaskStatus;
using System.Data.Entity.Infrastructure;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database
{
    public interface IWorkFlowDbContext : IDisposable
    {
        IDbSet<CompanyInformation> CompanyInformation { get; set; }
        IDbSet<ProjectStatus> ProjectStatus { get; set; }
        IDbSet<RegistrationToken> RegistrationToken { get; set; }
        IDbSet<SessionToken> SessionToken { get; set; }
        IDbSet<TaskStatus> TaskStatus { get; set; }
        IDbSet<UserAutoReport> UserAutoReport { get; set; }
        IDbSet<UserInformation> UserInformation { get; set; }
        IDbSet<UserProject> UserProject { get; set; }
        IDbSet<UserProjectRelation> UserProjectRelation { get; set; }
        IDbSet<UserReport> UserReport { get; set; }
        IDbSet<UserRole> UserRole { get; set; }
        IDbSet<UserTask> UserTask { get; set; }
        Task<int> SaveChangesAsync();
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }

    public class WorkFlowDbContext : DbContext, IWorkFlowDbContext
    {
        public IDbSet<CompanyInformation> CompanyInformation { get; set; }
        public IDbSet<ProjectStatus> ProjectStatus { get; set; }
        public IDbSet<RegistrationToken> RegistrationToken { get; set; } 
        public IDbSet<SessionToken> SessionToken { get; set; } 
        public IDbSet<TaskStatus> TaskStatus { get; set; }
        public IDbSet<UserAutoReport> UserAutoReport { get; set; }
        public IDbSet<UserInformation> UserInformation { get; set; }
        public IDbSet<UserProject> UserProject { get; set; }
        public IDbSet<UserProjectRelation> UserProjectRelation { get; set; }
        public IDbSet<UserReport> UserReport { get; set; }
        public IDbSet<UserRole> UserRole { get; set; }
        public IDbSet<UserTask> UserTask { get; set; }

        static WorkFlowDbContext()
        {
            System.Data.Entity.Database.SetInitializer<WorkFlowDbContext>(null);
        }

        public WorkFlowDbContext()
            : base("Name=WorkFlowConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public WorkFlowDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CompanyInformationConfiguration());
            modelBuilder.Configurations.Add(new ProjectStatusConfiguration());
            modelBuilder.Configurations.Add(new RegistrationTokenConfiguration());
            modelBuilder.Configurations.Add(new SessionTokenConfiguration());
            modelBuilder.Configurations.Add(new TaskStatusConfiguration());
            modelBuilder.Configurations.Add(new UserAutoReportConfiguration());
            modelBuilder.Configurations.Add(new UserInformationConfiguration());
            modelBuilder.Configurations.Add(new UserProjectConfiguration());
            modelBuilder.Configurations.Add(new UserProjectRelationConfiguration());
            modelBuilder.Configurations.Add(new UserReportConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new UserTaskConfiguration());
        }            
    }
}



