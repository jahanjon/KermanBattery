using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using RepresentativePanel.Application.Contract.Auth;
using RepresentativePanel.Application.Contract.Auth.Dto;
namespace RepresentativePanel.Application.Service
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(TokenParametersDto tokenParameters, string JwtKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.NameIdentifier, tokenParameters.PhoneNumber),
             new Claim(ClaimTypes.Role, Convert.ToString(tokenParameters.Role)),
              }),
                Expires = DateTime.UtcNow.AddMonths(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
