using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Configurations
{
    public class RegistrationTokenConfiguration : EntityTypeConfiguration<RegistrationToken>
    {
        public RegistrationTokenConfiguration()
        {
            ToTable("dbo.RegistrationToken");
            HasKey(x => x.RegistrationTokenId);

            Property(x => x.RegistrationTokenId).HasColumnName("RegistrationTokenId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Token).HasColumnName("Token").IsRequired().HasMaxLength(32);
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            
            HasRequired(a => a.UserInformation).WithMany(b => b.RegistrationToken).HasForeignKey(c => c.UserId);
        }
    }
}
