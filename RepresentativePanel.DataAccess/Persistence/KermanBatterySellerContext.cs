using KermanBatterySeller.Domain.Entity.Battery;
using KermanBatterySeller.Domain.Entity.Event;
using KermanBatterySeller.Domain.Entity.Products;
using KermanBatterySeller.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using RepresentativePanel.DataAccess.Mapping;
using RepresentativePanel.Domain.Entity.SellerAgg;
using RepresentativePanel.Domain.Entity.SellerLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.DataAccess.Persistence
{
    public class KermanBatterySellerContext : DbContext
    {
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SellerLogin> UserLogins { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<BatteryDetails> BatteryDetails { get; set; }
        public KermanBatterySellerContext(DbContextOptions<KermanBatterySellerContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder?.ApplyConfiguration(new SellerConfiguration());
            builder?.ApplyConfiguration(new ProductConfiguration());
            builder?.ApplyConfiguration(new EventTypeConfiguration());
            builder?.ApplyConfiguration(new BatteryDetailsConfiguration());
            base.OnModelCreating(builder);


        }
    }
}
