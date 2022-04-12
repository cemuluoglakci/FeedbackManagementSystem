using CoreFMS.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationFMS.Helpers
{
    public static class TokenHelper
    {
        private static readonly string _key = Tools.ConfigurationManager.AppSetting["JwtSetting:Secret"];
        public static string generateJwtToken(User account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(SetClaims(account)),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static IEnumerable<Claim> SetClaims(User account)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Id", account.Id.ToString()));
            claims.Add(new Claim("name", account.FirstName));
            claims.Add(new Claim("email", account.Email));
            return claims;
        }
    }
}
