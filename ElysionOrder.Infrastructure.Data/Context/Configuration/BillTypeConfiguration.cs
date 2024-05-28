using ElysionOrder.Domain.Entitys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElysionOrder.Infrastructure.Data.Context.Configuration
{
    

    public class BillTypeConfiguration : IEntityTypeConfiguration<BillType>
    {
        public void Configure(EntityTypeBuilder<BillType> builder)
        {
            builder.ToTable("BillTypes");
        }
    }
}
