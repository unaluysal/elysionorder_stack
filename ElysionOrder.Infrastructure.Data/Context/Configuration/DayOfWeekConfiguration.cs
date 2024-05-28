using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DayOfWeek = ElysionOrder.Domain.Entitys.DayOfWeek;

namespace ElysionOrder.Infrastructure.Data.Context.Configuration
{

    public class DayOfWeekConfiguration : IEntityTypeConfiguration<DayOfWeek>
    {
        public void Configure(EntityTypeBuilder<DayOfWeek> builder)
        {
            builder.ToTable("DayOfWeeks");
        }
    }
}
