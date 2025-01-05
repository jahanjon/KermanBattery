using Microsoft.EntityFrameworkCore;
using RepresentativePanel.Domain.Entity.SellerLogin;
using RepresentativePanel.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.DataAccess.Repository
{
    public class SellerLoginRepository : ISellerLoginRepository
    {
        private readonly IGenericRepository<SellerLogin> sellerLogin;

        public SellerLoginRepository(IGenericRepository<SellerLogin> sellerLogin)
        {
            this.sellerLogin = sellerLogin;
        }

        public async Task<SellerLogin> GetActiveLoginAsync(string phoneNumber)
        {
            var result = await sellerLogin.GetEntities().Where(x => x.PhoneNumber == phoneNumber && x.LogoutTime == null).FirstOrDefaultAsync();
            return result;
        }
    }
}
