using RepresentativePanel.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Application.Contract.Seller.Auth
{
    public interface IAuthDataService
    {
        Task<Result<TokenResultDto>> Login(LoginDto loginDto, string jwtKey);
    }
}
