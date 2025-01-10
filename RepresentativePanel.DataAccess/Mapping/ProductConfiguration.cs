using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using KermanBatterySeller.Domain.Entity.Products;

namespace KermanBatterySeller.Infrastructure.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.SerialNumber).IsRequired().HasMaxLength(50);
            builder.Property(p => p.SaleDate).IsRequired();
            builder.HasOne(p => p.EventType).WithMany(e => e.Products).HasForeignKey(p => p.ProductEventTypeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Seller).WithMany().HasForeignKey(p => p.SellerId);
            builder.HasOne(p => p.BatteryDetails).WithMany().HasForeignKey(p => p.ProductBatteryDetailsId);

        }
    }
}
