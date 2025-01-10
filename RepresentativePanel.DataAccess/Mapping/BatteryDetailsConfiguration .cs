using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KermanBatterySeller.Domain.Entity.Battery;

namespace KermanBatterySeller.Infrastructure.Mapping
{
    public class BatteryDetailsConfiguration : IEntityTypeConfiguration<BatteryDetails>
    {
        public void Configure(EntityTypeBuilder<BatteryDetails> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Description).HasMaxLength(500);
            builder.Property(b => b.Capacity).IsRequired();
            builder.Property(b => b.Manufacturer).HasMaxLength(200);
        }
    }
}
