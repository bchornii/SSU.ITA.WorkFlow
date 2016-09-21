using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Configurations
{
    public class UserProjectRelationConfiguration : EntityTypeConfiguration<UserProjectRelation>
    {
        public UserProjectRelationConfiguration()
        {
            ToTable("dbo.UserProjectRelation");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.ProjectId).HasColumnName("ProjectId").IsRequired();

            HasRequired(a => a.UserInformation).WithMany(b => b.UserProjectRelation).HasForeignKey(c => c.UserId);
            HasRequired(a => a.UserProject).WithMany(b => b.UserProjectRelation).HasForeignKey(c => c.ProjectId);
        }
    }
}
