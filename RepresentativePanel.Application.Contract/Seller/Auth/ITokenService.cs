using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Application.Contract.Seller.Auth
{
    public interface ITokenService
    {
        string GenerateToken(TokenParametersDto tokenParameters, string JwtKey);
    }
}
