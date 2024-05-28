using ElysionOrder.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElysionOrder.Infrastructure.Data.Context.Configuration
{
    public class RouteUserConfiguration : IEntityTypeConfiguration<RouteUser>
    {
        public void Configure(EntityTypeBuilder<RouteUser> builder)
        {
            builder.ToTable("RouteUsers");
        }
    }
}
