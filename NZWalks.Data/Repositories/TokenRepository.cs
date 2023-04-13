using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NZWalks.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private IConfiguration _configuration;
        public TokenRepository(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            //Create claims
            var claim = new List<Claim>();

            claim.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach(var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audienc"],
                claim,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
