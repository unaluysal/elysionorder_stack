using ElysionOrder.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElysionOrder.Infrastructure.Data.Context.Configuration
{

    public class RoleRightConfiguration : IEntityTypeConfiguration<RoleRight>
    {
        public void Configure(EntityTypeBuilder<RoleRight> builder)
        {
            builder.ToTable("RoleRights");
        }
    }
}
