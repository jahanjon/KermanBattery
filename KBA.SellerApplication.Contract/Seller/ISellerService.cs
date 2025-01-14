using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Seller;

namespace KBA.SellerApplication.Contract.Seller
{
    public interface ISellerService
    {
        Task<Result<DashboardDto>> GetSellerData(int sellerId);
        Task<Result<DashboardDto>> UpdateProfileAsync(int sellerId);
        Task<Result<bool>> UpdateAndInsertProfile(DashboardDto dashboard,int sellerId);
    }
}
