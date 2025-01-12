using KBA.Framework.Core;

namespace KBA.SellerApplication.Contract.Seller
{
    public interface ISellerService
    {
        Task<Result<DashboardDto>> GetSellerData(int sellerId);
        Task<Result<DashboardDto>> UpdateProfileAsync(int sellerId);
        Task<Result<bool>> UpdateAndInsertProfile(DashboardDto dashboard);
    }
}
