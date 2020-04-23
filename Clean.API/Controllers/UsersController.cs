using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Clean.API.Models.Account;
using Clean.API.Results;
using Clean.Common;
using Clean.Persistence.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Clean.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public SignInManager<AppUser> SignInManager { get; }
        public UserManager<AppUser> UserManager { get; }
        public UsersController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest model)
        {
            var rs = new LoginResponse
            {
                Valid = false
            };

            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = await UserManager.FindByNameAsync(model.UserName);
                rs.Token = GenerateJwtToken(model.UserName, appUser);
                rs.Valid = true;
                rs.UserInfo = new UserInfoModel
                {
                    ID = appUser.Id,
                    Name = appUser.FirstName,
                    LastName = appUser.LastName
                };
                return Ok(rs);
            }
            else
            {
                return NotFound(rs);
            }
        }


        private string GenerateJwtToken(string username, AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JWTSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(AppConfig.JWTExpireDays);

            var token = new JwtSecurityToken(
                AppConfig.JWTIssuer,
                AppConfig.JWTIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}