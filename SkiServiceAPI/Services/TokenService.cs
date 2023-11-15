using Microsoft.IdentityModel.Tokens;
using SkiServiceAPI.Data;
using SkiServiceAPI.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SkiServiceAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
        }

        public string CreateToken(string username, RoleNames role)
        {
            //Creating Claims. You can add more information in these claims. For example email id.
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, username),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            //Creating credentials. Specifying which type of Security Algorithm we are using
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //Creating Token description
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Finally returning the created token
            return tokenHandler.WriteToken(token);
        }
    }
}
