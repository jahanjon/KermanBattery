using RepresentativePanel.Application.Contract.Auth;
using RepresentativePanel.Domain.Entity.SellerAgg;
using RepresentativePanel.Domain.Entity.SellerLogin;
using RepresentativePanel.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var existingLogin = await sellerLoginRepository.GetActiveLoginAsync(phoneNumber);
            if (existingLogin != null)
            {
                throw new InvalidOperationException("User is already logged in.");
            }
            var getSellerId= sellerRepository.GetEntities().FirstOrDefault(x=>x.PhoneNumber== phoneNumber);
            var newLogin = new SellerLogin(phoneNumber, ipAddress,getSellerId.Id);
            await sellerLoginService.AddEntity(newLogin);
            await sellerLoginService.SaveChange();
        }

        public async Task RecordLogoutAsync(string phoneNumber)
        {
            var activeLogin = await sellerLoginRepository.GetActiveLoginAsync(phoneNumber);

            if (activeLogin == null)
            {
                throw new InvalidOperationException("No active login found for this user.");
            }

            activeLogin.SetLogoutTime();
            await sellerLoginService.SaveChange();
        }
    }
}
