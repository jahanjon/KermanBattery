using KBA.Domain.Entity.Battery;
using KBA.Domain.Entity.Event;
using KBA.Domain.Entity.Products;
using KBA.SellerInfrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using KBA.SellerInfrastructure.Mapping;
using KBA.Domain.Entity.SellerAgg;
using KBA.Domain.Entity.SellerLogin;


namespace KBA.SellerInfrastructure.Persistence
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
