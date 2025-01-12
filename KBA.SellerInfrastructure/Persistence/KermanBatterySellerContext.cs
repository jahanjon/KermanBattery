using KBA.SellerInfrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using KBA.Domain.Entity.SellerAgg;
using KBA.Domain.Entity.SellerLogin;


namespace KBA.SellerInfrastructure.Persistence
{
    public class KermanBatterySellerContext : DbContext
    {
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SellerLoginReport> SellerLoginReport { get; set; }
        public KermanBatterySellerContext(DbContextOptions<KermanBatterySellerContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder?.ApplyConfiguration(new SellerConfiguration());
            builder?.ApplyConfiguration(new SellerLoginReportConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
