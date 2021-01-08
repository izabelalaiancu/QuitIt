using DataLayer.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(string email);
    }

    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;

        public TokenService(UserManager<User> userManager, RoleManager<Role> roleManager,
            IConfiguration config)
        {//lalala
            _roleManager = roleManager;
            _userManager = userManager;
            _config = config;
        }

        public async Task<string> GenerateAccessToken(string email) 
        {
            var user = await _userManager.FindByEmailAsync(email);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecurityKey"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            IList<Claim> claims = await _userManager.GetClaimsAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

                var r = await _roleManager.FindByNameAsync(role);
                var rclaims = await _roleManager.GetClaimsAsync(r);

                foreach (var rclaim in rclaims)
                {
                    claims.Add(rclaim);
                }
            }

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1440),
                SigningCredentials = creds
            };
            SecurityToken token = handler.CreateToken(tokenDescriptor);
            string tokenString = handler.WriteToken(token);

            return tokenString;
        }
    }
}
