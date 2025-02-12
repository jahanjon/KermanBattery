﻿using KBA.Domain.Entity.SellerAgg;
using System.Security.Cryptography;
using System.Text;

namespace KBA.SellerInfrastructure.Repository
{
    public class SellerRepository : ISellerRepository
    {
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedInput = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hashedInput == hashedPassword;
            }
        }
    }
}
