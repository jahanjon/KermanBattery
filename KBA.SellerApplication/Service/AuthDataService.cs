using KBA.Farmework.Core;
using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Auth;
using KBA.SellerApplication.Contract.Auth.Dto;
using KBA.Domain.Entity.SellerAgg;
using KBA.Domain.Enum;
using KBA.Domain.Repository;


namespace KBA.SellerApplication.Service
{
    public class AuthDataService : IAuthDataService
    {
        private readonly IGenericRepository<Seller> selllerRepsoitory;
        private readonly ISellerRepository hasherPassword;
        private readonly ISellerLoginService sellerLoginService;

        public AuthDataService(IGenericRepository<Seller> selllerRepsoitory, ISellerRepository hasherPassword, ISellerLoginService sellerLoginService)
        {
            this.selllerRepsoitory = selllerRepsoitory;
            this.hasherPassword = hasherPassword;
            this.sellerLoginService = sellerLoginService;
        }

        public async Task<Result<TokenResultDto>> Login(LoginDto loginDto, string jwtKey)
        {
            var user = await selllerRepsoitory.Find(u => u.PhoneNumber == loginDto.PhoneNumber);

            if (user == null)
            {

                user = new Seller(
                    phoneNumber: loginDto.PhoneNumber,
                    role: Roles.Seller,
                    isDeleted: false
                );


                user.ChangePassword(loginDto.Password, hasherPassword);


                await selllerRepsoitory.Insert(user);
            }
            else
            {

                if (!hasherPassword.VerifyHashedPassword(user.Password, loginDto.Password))
                {
                    return Result<TokenResultDto>.Failure(ResultInfo.IncorrectEmailOrPassword);
                }


                if (user.IsDeleted)
                {
                    return Result<TokenResultDto>.Failure(ResultInfo.UserInactive);
                }
            }


            var tokenParameters = new TokenParametersDto
            {
                Id = user.Id,
                Role = (int)user.Role,
            };

            var tokenService = new TokenService();
            var token = tokenService.GenerateToken(tokenParameters, jwtKey);
            var loginDtoResult = new TokenResultDto
            {
                Token = token
            };

            return Result<TokenResultDto>.Success(ResultInfo.LoginSuccess, loginDtoResult);
        }

        public async Task<Result<string>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await selllerRepsoitory.Find(x => x.PhoneNumber == changePasswordDto.PhoneNumber);

            if (user == null)
            {
                return Result<string>.Failure(ResultInfo.UserPhoneNumberNotFound);
            }

            if (!user.ValidateOtpCode(changePasswordDto.VerificationCode))
            {
                return Result<string>.Failure(ResultInfo.InvalidOrExpiredConfirmationCode);
            }

            user.ChangePassword(changePasswordDto.NewPassword, hasherPassword);
            user.SetOtpCode(null, DateTime.MinValue);

            await selllerRepsoitory.Update(user);

            return Result<string>.Success(ResultInfo.PasswordChanged);
        }

        public async Task<Result<string>> GetVerificationCode(GetverificationCodeDto getverification)
        {
            var user = await selllerRepsoitory.Find(x => x.PhoneNumber == getverification.PhoneNumber);

            if (user == null)
            {
                return Result<string>.Failure(ResultInfo.UserPhoneNumberNotFound);
            }

            var verificationCode = (123456).ToString();

            user.SetOtpCode(verificationCode, DateTime.Now.AddMinutes(10));

            await selllerRepsoitory.Update(user);

            return Result<string>.Success(ResultInfo.ConfirmationCodeGenerated);
        }

        public async Task<Result<bool>> ChangeRoleToAdminKP(int userId)
        {

            var seller = await selllerRepsoitory.Find(x => x.Id == userId);

            if (seller == null)
            {
                return Result<bool>.Failure(ResultInfo.SellerNotFound);
            }


            seller.ChangeRole(Roles.Admin);


            await selllerRepsoitory.Update(seller);

            return Result<bool>.Success(ResultInfo.OperationSuccess);
        }
    }
}
