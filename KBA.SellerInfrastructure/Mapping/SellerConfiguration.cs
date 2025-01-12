using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KBA.Domain.Entity.SellerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerInfrastructure.Mapping
{
    public class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Title).HasMaxLength(25);
            builder.Property(s => s.Province).HasMaxLength(100);
            builder.Property(s => s.Address).HasMaxLength(400);
            builder.Property(s => s.City).HasMaxLength(100);
            builder.Property(s => s.Description).HasMaxLength(455);

            builder.Property(s => s.FirstName).HasMaxLength(50);
            builder.Property(s => s.LastName).HasMaxLength(50);
            builder.Property(s => s.Email).HasMaxLength(100);
            builder.Property(s => s.Password).IsRequired().HasMaxLength(255);
            builder.Property(s => s.NationalNumber).HasMaxLength(10);
            builder.Property(s => s.OtpCode).HasMaxLength(6);
            builder.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(11);
        }
    }
}
