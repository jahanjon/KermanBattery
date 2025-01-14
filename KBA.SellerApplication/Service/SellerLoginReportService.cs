using AutoMapper;
using KBA.Farmework.Core;
using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Auth;
using KBA.SellerApplication.Contract.Auth.Dto;
using KBA.Domain.Entity.SellerAgg;
using KBA.Domain.Entity.SellerLogin;
using KBA.Domain.Repository;
using KBA.SellerInfrastructure.Repository;
using KBA.SellerApplication.Contract.Seller;

namespace KBA.SellerApplication.Service
{
    public class SellerLoginReportService : ISellerLoginReportService
    {
        private readonly IGenericRepository<SellerLoginReport> sellerLoginService;
        private readonly IGenericRepository<Seller> sellerRepository;
        private readonly ISellerLoginReportRepository sellerLoginRepository;
        private readonly IMapper mapper;

        public SellerLoginReportService(IGenericRepository<SellerLoginReport> sellerLoginService, ISellerLoginReportRepository sellerLoginRepository, IGenericRepository<Seller> sellerRepository, IMapper mapper)
        {
            this.sellerLoginService = sellerLoginService;
            this.sellerLoginRepository = sellerLoginRepository;
            this.sellerRepository = sellerRepository;
            this.mapper = mapper;
        }
        public async Task RecordLoginAsync(int sellerId, string ipAddress)
        {

            var getSeller = await sellerRepository.FindAsNoTracking(x => x.Id == sellerId);

            if (getSeller != null)
            {

                var existingLogin = await sellerLoginService.Find(x => x.SellerId == getSeller.Id && x.LogoutTime == null);

                if (existingLogin != null)
                {

                    existingLogin.LastUpdateDate = DateTime.UtcNow;
                    existingLogin.UpdateIpAddress(ipAddress);
                    await sellerLoginService.Update(existingLogin);
                }
                else
                {

                    var newLogin = new SellerLoginReport(ipAddress, getSeller.Id);
                    await sellerLoginService.Insert(newLogin);
                }
            }
        }




        public async Task RecordLogoutAsync(int sellerId)
        {
            var activeLogin = await sellerLoginRepository.GetActiveLoginAsync(sellerId);

            if (activeLogin == null)
            {
                throw new InvalidOperationException("No active login found for this user.");
            }

            activeLogin.SetLogoutTime();
            await sellerLoginService.Update(activeLogin);
        }

        public async Task<Result<SellerLoginReportDto>> SellerActivity(int sellerId)
        {
            var sellerData = await sellerLoginService.FindAsNoTracking(x => x.SellerId == sellerId);

            if (sellerData == null)
            {
                return Result<SellerLoginReportDto>.Failure(ResultInfo.SellerNotFound);
            }

            var sellerActivity = mapper.Map<SellerLoginReportDto>(sellerData);

            return Result<SellerLoginReportDto>.Success(ResultInfo.OperationSuccess, sellerActivity);
        }






    }
}
