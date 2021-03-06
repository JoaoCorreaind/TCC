using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace WebApplication1.Services
{
    public static class TokenServices
    {
        public static string GenerateToken(User user)
        {
            var token = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("E271BF74D722C245674175A739E7C");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var t = token.CreateToken(tokenDescriptor);
            return token.WriteToken(t);
        }
    }
}
