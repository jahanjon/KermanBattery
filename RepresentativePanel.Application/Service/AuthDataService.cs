using KermanBattery.Farmework.Domain;
using Microsoft.EntityFrameworkCore;
using RepresentativePanel.Application.Contract.Seller.Auth;
using RepresentativePanel.Domain.Core;
using RepresentativePanel.Domain.Enum;
using RepresentativePanel.Domain.SellerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Application.Service
{
    public class AuthDataService : IAuthDataService
    {
        private readonly IGenericRepository<Seller> selllerRepsoitory;
        private readonly ISellerRepository hasherPassword;
        public async Task<Result<TokenResultDto>> Login(LoginDto loginDto, string jwtKey)
        {
            var user = await selllerRepsoitory.GetEntities()
                .SingleOrDefaultAsync(u => u.PhoneNumber == loginDto.PhoneNumber);

            if (user == null)
            {

                user = new Seller(
                    phoneNumber: loginDto.PhoneNumber,
                    role: Roles.Seller,
                    isDeleted: false
                );


                user.ChangePassword(loginDto.Password, hasherPassword);


                await selllerRepsoitory.AddEntity(user);
                await selllerRepsoitory.SaveChange();
            }
            else
            {

                if (!hasherPassword.VerifyHashedPassword(user.Password, loginDto.Password))
                {
                    return Result<TokenResultDto>.Failure(-400, "ایمیل یا رمز عبور اشتباه است");
                }


                if (user.IsDeleted)
                {
                    return Result<TokenResultDto>.Failure(-400, "کاربر غیرفعال است. لطفاً با پشتیبانی تماس بگیرید.");
                }
            }


            var tokenParameters = new TokenParametersDto
            {
                PhoneNumber = user.PhoneNumber,
                Role = (int)user.Role,
            };

            var tokenService = new TokenService();
            var token = tokenService.GenerateToken(tokenParameters, jwtKey);

            var loginDtoResult = new TokenResultDto
            {
                Token = token
            };

            return Result<TokenResultDto>.Success(200, "ورود موفقیت‌آمیز بود", loginDtoResult);
        }
    }
}
