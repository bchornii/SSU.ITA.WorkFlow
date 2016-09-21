using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Configurations
{
    public class ProjectStatusConfiguration : EntityTypeConfiguration<ProjectStatus>
    {
        public ProjectStatusConfiguration()
        {
            ToTable("dbo.ProjectStatus");
            HasKey(x => x.StatusId);

            Property(x => x.StatusId).HasColumnName("StatusId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(20);
        }
    }
}
