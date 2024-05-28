using ElysionOrder.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElysionOrder.Infrastructure.Data.Context.Configuration
{
    public class StoreTypeConfiguration : IEntityTypeConfiguration<StoreType>
    {
        public void Configure(EntityTypeBuilder<StoreType> builder)
        {
            builder.ToTable("StoreTypes");
        }
    }
}
