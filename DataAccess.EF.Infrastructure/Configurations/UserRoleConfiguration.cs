using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Configurations
{
    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            ToTable("dbo.UserRole");
            HasKey(x => x.RoleId);

            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(20);
        }
    }
}
