using AutoMapper;
using KBA.Farmework.Core;
using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Seller;
using KBA.Domain.Entity.SellerAgg;
using KBA.Domain.Repository;

namespace KBA.SellerApplication.Service
{
    public class SellerService : ISellerService
    {
        private readonly IGenericRepository<Seller> sellerRepsoiotry;
        private readonly IMapper mapper;

        public SellerService(IGenericRepository<Seller> sellerRepsoiotry, IMapper mapper)
        {
            this.sellerRepsoiotry = sellerRepsoiotry;
            this.mapper = mapper;
        }

        public async Task<Result<DashboardDto>> GetSellerData(int sellerId)
        {

            var seller = await sellerRepsoiotry.FindAsNoTracking(s => s.Id == sellerId);

            if (seller == null)
            {
                return Result<DashboardDto>.Failure(ResultInfo.OperationFailed);
            }


            var dashboardDto = mapper.Map<DashboardDto>(seller);
            return Result<DashboardDto>.Success(ResultInfo.OperationSuccess, dashboardDto);
        }


        public async Task<Result<bool>> UpdateAndInsertProfile(DashboardDto dashboardDto)
        {
            try
            {

                var seller = await sellerRepsoiotry.Find(x => x.Id == dashboardDto.SellerId);

                if (seller == null)
                {
                    return Result<bool>.Failure(ResultInfo.SellerNotFound);
                }


                seller.UpdateProfile(dashboardDto.Title, dashboardDto.Province, dashboardDto.Address);


                var updateResult = await sellerRepsoiotry.Update(seller);

                if (!updateResult)
                {
                    return Result<bool>.Failure(ResultInfo.FailedUpdate);
                }

                return Result<bool>.Success(ResultInfo.SuccessUpdate, true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ResultInfo.InternalServerError);
            }
        }


        public async Task<Result<DashboardDto>> UpdateProfileAsync(int sellerId)
        {
            var seller = await sellerRepsoiotry.Find(x => x.Id == sellerId);

            if (seller == null)
            {
                return Result<DashboardDto>.Failure(ResultInfo.SellerNotFound);
            }

            var dashboardDto = mapper.Map<DashboardDto>(seller);
            return Result<DashboardDto>.Success(ResultInfo.OperationSuccess, dashboardDto);
        }

    }
}
