using Microsoft.IdentityModel.Tokens;
using SkiServiceAPI.Data;
using SkiServiceAPI.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SkiServiceAPI.DTOs;

namespace SkiServiceAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
#pragma warning disable CS8604 // Mögliches Nullverweisargument.
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
#pragma warning restore CS8604 // Mögliches Nullverweisargument.
        }

        public TokenData CreateToken(string id, string username, RoleNames role)
        {
            //Creating Claims. You can add more information in these claims. For example email id.
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, id),
                new Claim(ClaimTypes.NameIdentifier, username),
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
            return new TokenData { 
                Token = tokenHandler.WriteToken(token),
                Expires = token.ValidTo
            };
        }
    }
}
