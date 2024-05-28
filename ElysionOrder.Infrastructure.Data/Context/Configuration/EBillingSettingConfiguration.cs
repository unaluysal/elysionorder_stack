using ElysionOrder.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElysionOrder.Infrastructure.Data.Context.Configuration
{

    public class EBillingSettingConfiguration : IEntityTypeConfiguration<EBillSetting>
    {
        public void Configure(EntityTypeBuilder<EBillSetting> builder)
        {
            builder.ToTable("EBillSettings");
        }
    }
}
