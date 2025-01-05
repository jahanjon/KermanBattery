﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepresentativePanel.Domain.Entity.SellerLogin;


namespace RepresentativePanel.DataAccess.Mapping
{
    public class SellerLoginConfiguration : IEntityTypeConfiguration<SellerLogin>
    {
        public void Configure(EntityTypeBuilder<SellerLogin> builder)
        {
            builder.ToTable("UserLogins");

            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.Seller).WithMany(s => s.SellerLogins).HasForeignKey(u => u.SellerId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(15);

            builder.Property(u => u.LoginTime).IsRequired();

            builder.Property(u => u.IpAddress).IsRequired().HasMaxLength(50);

            builder.Property(u => u.LogoutTime).IsRequired(false);
        }
    }

}
