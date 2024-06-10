using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstApiMVC.jwttoken
{

    public class TokenService
    {
        private readonly string _secret;

        public TokenService(string secret)
        {
            _secret = secret;
        }
        public string GenerateToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject =new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, userId),

                }),
                Expires =DateTime.UtcNow.AddMinutes(1),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescription); 
            return tokenHandler.WriteToken(token);
        }
    }
}
