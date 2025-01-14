using KBA.Domain.Entity.SellerLogin;
using KBA.Domain.Repository;

namespace KBA.SellerInfrastructure.Repository
{
    public class SellerLoginReportRepository : ISellerLoginReportRepository
    {
        private readonly IGenericRepository<SellerLoginReport> sellerLogin;

        public SellerLoginReportRepository(IGenericRepository<SellerLoginReport> sellerLogin)
        {
            this.sellerLogin = sellerLogin;
        }

        public async Task<SellerLoginReport> GetActiveLoginAsync(int sellerId)
        {
            var result = await sellerLogin.Find(x => x.SellerId == sellerId);
            return result;
        }
    }
}
