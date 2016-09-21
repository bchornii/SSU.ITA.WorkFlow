using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Configurations
{
    public class UserAutoReportConfiguration : EntityTypeConfiguration<UserAutoReport>
    {
        public UserAutoReportConfiguration()
        {
            ToTable("dbo.UserAutoReport");
            HasKey(x => x.AutoReportId);

            Property(x => x.AutoReportId).HasColumnName("AutoReportId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TaskId).HasColumnName("TaskId").IsRequired();
            Property(x => x.IsActive).HasColumnName("IsActive").IsRequired();
            Property(x => x.FromTime).HasColumnName("FromTime").IsRequired();
            Property(x => x.ToTime).HasColumnName("ToTime").IsRequired();
            Property(x => x.RepeatDays).HasColumnName("RepeatDays").IsRequired().HasMaxLength(100);
            Property(x => x.StartDate).HasColumnName("StartDate").IsRequired();
            Property(x => x.EndDate).HasColumnName("EndDate").IsRequired();
            Property(x => x.Comment).HasColumnName("Comment").IsOptional().HasMaxLength(150);

            HasRequired(a => a.UserTask).WithMany(b => b.UserAutoReport).HasForeignKey(c => c.TaskId);
        }
    }
}
