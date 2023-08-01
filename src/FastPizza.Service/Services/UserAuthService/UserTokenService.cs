using FastPizza.Domain.Entities.Users;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Interfaces.UserAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FastPizza.Service.Services.UserAuthService
{
    public class UserTokenService : ITokenUserService
    {
        private readonly IConfiguration _config;

        public UserTokenService(IConfiguration configuration)
        {
            this._config = configuration;
        }
        public string GenereateToken(User user)
        {
            var identityClaims = new Claim[]
            {
            new Claim("Id", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("Lastname", user.LastName),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.Role, user.IdentityRole.ToString())
       };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("de812cfb-5aec-4c72-8c42-b506efa878d2"!));
            var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            int expiresHours = 24!;
            var token = new JwtSecurityToken(
                issuer: "https://fastpizza.uz",
                audience: "Fast Pizza",
                claims: identityClaims,
                expires: TimeHelper.GetDateTime().AddHours(expiresHours),
                signingCredentials: keyCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
