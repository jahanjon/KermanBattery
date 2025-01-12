using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using KBA.SellerApplication.Contract.Auth.Dto;
using KBA.SellerApplication.Contract.Auth;
namespace KBA.SellerApplication.Service
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
                
                new Claim("UserId", tokenParameters.Id.ToString()),
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
