using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Configurations
{
    public class UserProjectConfiguration : EntityTypeConfiguration<UserProject>
    {
        public UserProjectConfiguration()
        {
            ToTable("dbo.UserProject");
            HasKey(x => x.ProjectId);

            Property(x => x.ProjectId).HasColumnName("ProjectId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.StatusId).HasColumnName("StatusId").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            Property(x => x.CreatorId).HasColumnName("CreatorId").IsRequired();
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.CreateDate).HasColumnName("CreateDate").IsRequired();
            Property(x => x.EndDate).HasColumnName("EndDate").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(150);

            HasRequired(a => a.ProjectStatus).WithMany(b => b.UserProject).HasForeignKey(c => c.StatusId);
        }
    }
}
