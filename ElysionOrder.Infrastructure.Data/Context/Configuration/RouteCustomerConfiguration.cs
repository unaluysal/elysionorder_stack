using ElysionOrder.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElysionOrder.Infrastructure.Data.Context.Configuration
{
    public class RouteCustomerConfiguration : IEntityTypeConfiguration<RouteCustomer>
    {
        public void Configure(EntityTypeBuilder<RouteCustomer> builder)
        {
            builder.ToTable("RouteCustomers");
        }
    }
}
