using KBA.Domain.Entity.SellerLogin;
using KBA.Domain.Repository;

namespace KBA.SellerInfrastructure.Repository
{
    public class SellerLoginRepository : ISellerLoginRepository
    {
        private readonly IGenericRepository<SellerLogin> sellerLogin;

        public SellerLoginRepository(IGenericRepository<SellerLogin> sellerLogin)
        {
            this.sellerLogin = sellerLogin;
        }

        public async Task<SellerLogin> GetActiveLoginAsync(int sellerId)
        {
            var result = await sellerLogin.Find(x => x.Id == sellerId);
            return result;
        }
    }
}
