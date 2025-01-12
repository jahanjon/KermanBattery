using KBA.Framework.Core;
using KBA.SellerApplication.Contract.Auth.Dto;

namespace KBA.SellerApplication.Contract.Auth
{
    public interface IAuthDataService
    {
        Task<Result<TokenResultDto>> Login(LoginDto loginDto, string jwtKey);
        Task<Result<string>> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<Result<string>> GetVerificationCode(GetverificationCodeDto getverification);
        Task<Result<bool>> ChangeRoleToAdminKP(int userId);
    }
}
