using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Configurations
{
    public class SessionTokenConfiguration : EntityTypeConfiguration<SessionToken>
    {
        public SessionTokenConfiguration()
        {
            ToTable("dbo.SessionToken");
            HasKey(x => x.SessionTokenId);

            Property(x => x.SessionTokenId).HasColumnName("SessionTokenId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Token).HasColumnName("Token").IsRequired().HasMaxLength(32);
            Property(x => x.ExpirationDate).HasColumnName("ExpirationDate").IsRequired();
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
                    
            HasRequired(a => a.UserInformation).WithMany(b => b.SessionToken).HasForeignKey(c => c.UserId); 
        }
    }
}
