using Microsoft.IdentityModel.Tokens;
using SkiServiceAPI.Common;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Models;
using SkiServiceModels;
using SkiServiceModels.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SkiServiceAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IApplicationDBContext _dBContext;
        private readonly IConfiguration _configuration;

        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config, IApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
            _configuration = config;

            var key = _configuration["JWT:Key"];
            if (key == null) throw new Exception("JWT Key not found");
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public async Task<TokenData> CreateToken(User user, bool keep = true)
        {
            //Creating Claims. You can add more information in these claims. For example email id.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
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

            user.RefreshToken = keep ? GenerateRefreshToken() : null;
            await _dBContext.SaveChangesAsync();

            return new TokenData
            {
                Token = tokenHandler.WriteToken(token),
                Expires = token.ValidTo,
                RefreshToken = user.RefreshToken
            };
        }

        public async Task<TaskResult<RefreshResult>> RefreshToken(string token, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var userId = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return TaskResult<RefreshResult>.Error(LocalizationKey.INVALID_CREDENTIALS);

            var user = await _dBContext.Users.FindAsync(int.Parse(userId));
            if (user == null || user.RefreshToken != refreshToken) return TaskResult<RefreshResult>.Error(LocalizationKey.INVALID_CREDENTIALS);

            var tokenData = await CreateToken(user, true);
            return TaskResult<RefreshResult>.Success(new RefreshResult
            {
                TokenData = tokenData,
                User = user
            });
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var validateParams = Program.TokenValidationParameters(_configuration);
            validateParams.ValidateLifetime = false;

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, validateParams, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private string GenerateRefreshToken()
        {
            byte[] data = Guid.NewGuid().ToByteArray();
            using (var sha256 = new SHA256Managed())
            {
                byte[] hashData = sha256.ComputeHash(data);
                StringBuilder hexString = new StringBuilder(hashData.Length * 2);
                foreach (byte b in hashData)
                {
                    hexString.AppendFormat("{0:x2}", b);
                }

                return hexString.ToString();
            }
        }
    }
}
