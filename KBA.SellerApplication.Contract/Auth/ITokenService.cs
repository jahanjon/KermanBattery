using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBA.SellerApplication.Contract.Auth.Dto;

namespace KBA.SellerApplication.Contract.Auth
{
    public interface ITokenService
    {
        string GenerateToken(TokenParametersDto tokenParameters, string JwtKey);
    }
}
