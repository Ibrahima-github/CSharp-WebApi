using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebAPI.Helpers
{
    public class JwtService
    {
        public string securityKey = "this is my secure key";
        public string Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));

            var securityToken = new JwtSecurityToken(header, payload);

            try
            {
                return new JwtSecurityTokenHandler().WriteToken(securityToken);
            }
            catch(Exception e)
            {
                return e.Message;
            }
            
        }
        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securityKey);

            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer =false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
