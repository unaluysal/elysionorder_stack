using ElysionOrder.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElysionOrder.Infrastructure.Data.Context.Configuration
{

    public class PaymentWayConfiguration : IEntityTypeConfiguration<PaymentWay>
    {
        public void Configure(EntityTypeBuilder<PaymentWay> builder)
        {
            builder.ToTable("PaymentWays");
        }
    }
}
