using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Configurations
{
    public class UserInformationConfiguration : EntityTypeConfiguration<UserInformation>
    {
        public UserInformationConfiguration()
        {
            ToTable("dbo.UserInformation");
            HasKey(x => x.UserId);

            Property(x => x.UserId).HasColumnName("UserId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired();
            Property(x => x.CompanyId).HasColumnName("CompanyId").IsRequired();
            Property(x => x.ManagerId).HasColumnName("ManagerId").IsRequired();
            Property(x => x.Email).HasColumnName("Email").IsRequired().HasMaxLength(244);
            Property(x => x.Password).HasColumnName("Password").IsRequired().HasMaxLength(256);
            Property(x => x.Salt).HasColumnName("Salt").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasMaxLength(50);
            Property(x => x.SurName).HasColumnName("SurName").IsOptional().HasMaxLength(50);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(100);
            Property(x => x.PhoneNumber).HasColumnName("PhoneNumber").IsOptional().HasMaxLength(50);
            Property(x => x.Photo).HasColumnName("Photo").IsOptional().HasMaxLength(100);
            Property(x => x.IsConfirmed).HasColumnName("IsConfirmed").IsRequired();

            HasRequired(a => a.UserRole).WithMany(b => b.UserInformation).HasForeignKey(c => c.RoleId); 
            HasRequired(a => a.CompanyInformation).WithMany(b => b.UserInformation).HasForeignKey(c => c.CompanyId); 
        }
    }
}
