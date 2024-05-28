using ElysionOrder.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElysionOrder.Infrastructure.Data.Context.Configuration
{
    public class SalesStatusConfiguration : IEntityTypeConfiguration<SalesStatus>
    {
        public void Configure(EntityTypeBuilder<SalesStatus> builder)
        {
            builder.ToTable("SalesStatuses");
        }
    }
}
