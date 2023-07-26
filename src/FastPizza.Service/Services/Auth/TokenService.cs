using FastPizza.Domain.Entities.Customers;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FastPizza.Service.Services.Auth
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration configuration)
        {
            _config = configuration.GetSection("Jwt");
        }
        public string GenereateToken(Customer customer)
        {
            var identityClaims = new Claim[]
            {
            new Claim("Id", customer.Id.ToString()),
            new Claim("FullName", customer.FullName),
            new Claim(ClaimTypes.Email, customer.Email),
            new Claim(ClaimTypes.MobilePhone, customer.PhoneNumber)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("de812cfb-5aec-4c72-8c42-b506efa878d2"!));
            var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            int expiresHours = 24;
            var token = new JwtSecurityToken(
                issuer: _config["Issuer"],
                audience: _config["Audience"],
                claims: identityClaims,
                expires: TimeHelper.GetDateTime().AddHours(expiresHours),
                signingCredentials: keyCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
