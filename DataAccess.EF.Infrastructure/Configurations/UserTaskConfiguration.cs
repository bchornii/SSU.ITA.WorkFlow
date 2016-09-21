using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Configurations
{
    public class UserTaskConfiguration : EntityTypeConfiguration<UserTask>
    {
        public UserTaskConfiguration()
        {
            ToTable("dbo.UserTask");
            HasKey(x => x.TaskId);

            Property(x => x.TaskId).HasColumnName("TaskId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            Property(x => x.ProjectId).HasColumnName("ProjectId").IsRequired();
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.StatusId).HasColumnName("StatusId").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(150);

            HasRequired(a => a.UserProject).WithMany(b => b.UserTask).HasForeignKey(c => c.ProjectId);
            HasRequired(a => a.UserInformation).WithMany(b => b.UserTask).HasForeignKey(c => c.UserId);
            HasRequired(a => a.TaskStatus).WithMany(b => b.UserTask).HasForeignKey(c => c.StatusId);
        }
    }
}
