﻿using Microsoft.EntityFrameworkCore;
using RepresentativePanel.Application.Contract.Auth;
using RepresentativePanel.Domain.Entity.SellerLogin;
using RepresentativePanel.Domain.Repository;

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
            var result = await sellerLogin.Find(x => x.PhoneNumber == phoneNumber);
            return result;
        }
    }
}
