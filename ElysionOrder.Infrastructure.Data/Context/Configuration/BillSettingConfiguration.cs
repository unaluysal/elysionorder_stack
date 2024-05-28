using ElysionOrder.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElysionOrder.Infrastructure.Data.Context.Configuration
{
    public class BillSettingConfiguration : IEntityTypeConfiguration<BillSetting>
    {
        public void Configure(EntityTypeBuilder<BillSetting> builder)
        {
            builder.ToTable("BillSettings");
        }
    }
}
