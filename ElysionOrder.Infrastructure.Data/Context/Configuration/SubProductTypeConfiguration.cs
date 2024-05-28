using ElysionOrder.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElysionOrder.Infrastructure.Data.Context.Configuration
{
    public class SubProductTypeConfiguration : IEntityTypeConfiguration<SubProductType>
    {
        public void Configure(EntityTypeBuilder<SubProductType> builder)
        {
            builder.ToTable("SubProductTypes");
        }
    }
}
