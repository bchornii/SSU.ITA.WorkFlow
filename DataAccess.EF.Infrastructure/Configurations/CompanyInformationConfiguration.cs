using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Configurations
{
    public class CompanyInformationConfiguration : EntityTypeConfiguration<CompanyInformation>
    {
        public CompanyInformationConfiguration()
        {
            ToTable("dbo.CompanyInformation");
            HasKey(x => x.CompanyId);

            Property(x => x.CompanyId).HasColumnName("CompanyId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            Property(x => x.PhoneNumber).HasColumnName("PhoneNumber").IsOptional().HasMaxLength(50);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(100);
            Property(x => x.Email).HasColumnName("Email").IsOptional().HasMaxLength(244);
            Property(x => x.Desciption).HasColumnName("Desciption").IsOptional().HasMaxLength(150);
        }
    }
}
