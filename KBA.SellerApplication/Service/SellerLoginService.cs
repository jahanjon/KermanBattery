using AutoMapper;
using KBA.Farmework.Core;
using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Auth;
using KBA.SellerApplication.Contract.Auth.Dto;
using KBA.Domain.Entity.SellerAgg;
using KBA.Domain.Entity.SellerLogin;
using KBA.Domain.Repository;

namespace KBA.SellerApplication.Service
{
    public class SellerLoginService : ISellerLoginService
    {
        private readonly IGenericRepository<SellerLoginReport> sellerLoginService;
        private readonly IGenericRepository<Seller> sellerRepository;
        private readonly ISellerLoginReportRepository sellerLoginRepository;
        private readonly IMapper mapper;

        public SellerLoginService(IGenericRepository<SellerLoginReport> sellerLoginService, ISellerLoginReportRepository sellerLoginRepository, IGenericRepository<Seller> sellerRepository, IMapper mapper)
        {
            this.sellerLoginService = sellerLoginService;
            this.sellerLoginRepository = sellerLoginRepository;
            this.sellerRepository = sellerRepository;
            this.mapper = mapper;
        }
        public async Task RecordLoginAsync(string phoneNumber, string ipAddress)
        {

            var getSeller = await sellerRepository.FindAsNoTracking(x => x.PhoneNumber == phoneNumber);

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

                    var newLogin = new SellerLoginReport(phoneNumber, ipAddress, getSeller.Id);
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

        public async Task<Result<List<SellerLoginDto>>> SellerActivity()
        {

            var sellerLogins = await sellerLoginService.GetAllAsNoTracking();

            if (!sellerLogins.Any())
            {
                return Result<List<SellerLoginDto>>.Failure(ResultInfo.OperationFailed);
            }


            var sellerLoginDtos = sellerLogins.Select(sl => mapper.Map<SellerLoginDto>(sl)).ToList();

            return Result<List<SellerLoginDto>>.Success(ResultInfo.OperationSuccess, sellerLoginDtos);
        }


    }
}
