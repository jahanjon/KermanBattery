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
    public class RepresentativePanelContext : DbContext
    {
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SellerLogin> UserLogins { get; set; }
        public RepresentativePanelContext(DbContextOptions<RepresentativePanelContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder?.ApplyConfiguration(new SellerConfiguration());
            base.OnModelCreating(builder);


        }
    }
}
