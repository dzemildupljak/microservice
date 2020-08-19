using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using userApi.Repo;

namespace userApi.Models
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly UserContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public TokenGenerator(UserContext context,
                                UserManager<IdentityUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public string AccessJWToken(IdentityUser user, string role)
        {
            var keyJson = _configuration.GetSection("TokenConst:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyJson));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim (ClaimTypes.Role, role)

            };

            var issuerJson = _configuration.GetValue<string>("TokenConst:Issuer");
            var audienceJson = _configuration.GetValue<string>("TokenConst:Audience");

            var token = new JwtSecurityToken(
                issuerJson,
                audienceJson,
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: cred
            );
            var strToken = new JwtSecurityTokenHandler().WriteToken(token);
            return strToken;
        }
    }
}