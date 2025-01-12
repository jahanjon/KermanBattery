using KBA.Domain.Entity.SellerLogin;
using KBA.Domain.Repository;

namespace KBA.SellerInfrastructure.Repository
{
    public class SellerLoginRepository : ISellerLoginReportRepository
    {
        private readonly IGenericRepository<SellerLoginReport> sellerLogin;

        public SellerLoginRepository(IGenericRepository<SellerLoginReport> sellerLogin)
        {
            this.sellerLogin = sellerLogin;
        }

        public async Task<SellerLoginReport> GetActiveLoginAsync(int sellerId)
        {
            var result = await sellerLogin.Find(x => x.Id == sellerId);
            return result;
        }
    }
}
