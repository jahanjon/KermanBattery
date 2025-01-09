using RepresentativePanel.Application.Contract.Auth.Dto;
using RepresentativePanel.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Application.Contract.Auth
{
    public interface IAuthDataService
    {
        Task<Result<TokenResultDto>> Login(LoginDto loginDto, string jwtKey);
        Task<Result<string>> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<Result<string>> GetVerificationCode(GetverificationCodeDto getverification);
        Task<Result<bool>> ChangeRoleToAdminKP(int userId);
    }
}
