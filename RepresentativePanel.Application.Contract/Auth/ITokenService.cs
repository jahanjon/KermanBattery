using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepresentativePanel.Application.Contract.Auth.Dto;

namespace RepresentativePanel.Application.Contract.Auth
{
    public interface ITokenService
    {
        string GenerateToken(TokenParametersDto tokenParameters, string JwtKey);
    }
}
