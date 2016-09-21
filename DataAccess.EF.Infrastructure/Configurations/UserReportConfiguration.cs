using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Configurations
{
    public class UserReportConfiguration : EntityTypeConfiguration<UserReport>
    {
        public UserReportConfiguration()
        {
            ToTable("dbo.UserReport");
            HasKey(x => x.ReportId);

            Property(x => x.ReportId).HasColumnName("ReportId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TaskId).HasColumnName("TaskId").IsRequired();
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.CreateDate).HasColumnName("CreateDate").IsRequired();
            Property(x => x.StartTime).HasColumnName("StartTime").IsRequired();
            Property(x => x.EndTime).HasColumnName("EndTime").IsRequired();
            Property(x => x.Comment).HasColumnName("Comment").IsOptional().HasMaxLength(150);

            HasRequired(a => a.UserTask).WithMany(b => b.UserReport).HasForeignKey(c => c.TaskId);
            HasRequired(a => a.UserInformation).WithMany(b => b.UserReport).HasForeignKey(c => c.UserId);
        }
    }
}
