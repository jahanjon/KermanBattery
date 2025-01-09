using Microsoft.EntityFrameworkCore;
using RepresentativePanel.Application.Contract.Auth;
using RepresentativePanel.Domain.Core;
using RepresentativePanel.Domain.Entity.SellerAgg;
using RepresentativePanel.Domain.Entity.SellerLogin;
using RepresentativePanel.Domain.Repository;

namespace RepresentativePanel.Application.Service
{
    public class SellerLoginService : ISellerLoginService
    {
        private readonly IGenericRepository<SellerLogin> sellerLoginService;
        private readonly IGenericRepository<Seller> sellerRepository;
        private readonly ISellerLoginRepository sellerLoginRepository;

        public SellerLoginService(IGenericRepository<SellerLogin> sellerLoginService, ISellerLoginRepository sellerLoginRepository, IGenericRepository<Seller> sellerRepository)
        {
            this.sellerLoginService = sellerLoginService;
            this.sellerLoginRepository = sellerLoginRepository;
            this.sellerRepository = sellerRepository;
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

                    var newLogin = new SellerLogin(phoneNumber, ipAddress, getSeller.Id);
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
    }
}
