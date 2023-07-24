using FastPizza.Domain.Entities.Customers;
using FastPizza.Domain.Entities.Users;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
            new Claim(ClaimTypes.MobilePhone, customer.PhoneNumber)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
            var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            int expiresHours = int.Parse(_config["Lifetime"]!);
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
