using ElysionOrder.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElysionOrder.Infrastructure.Data.Context.Configuration
{
    public class IbanConfiguration : IEntityTypeConfiguration<Iban>
    {
        public void Configure(EntityTypeBuilder<Iban> builder)
        {
            builder.ToTable("Ibans");
        }
    }
}
