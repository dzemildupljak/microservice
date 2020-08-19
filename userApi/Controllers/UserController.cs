using System.Net;
using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using userApi.Models;
using userApi.Dto;
using userApi.DtoResponse;
using Microsoft.Extensions.Configuration;
using userApi.Repo;

namespace userApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;

        public UserController(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                RoleManager<IdentityRole> roleManager,
                                UserContext context,
                                IConfiguration configuration,
                                ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        // POST api/user/register
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] DtoUserRegister model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest(new UserResponse { Message = "Password and confirm password does not match.", IsSuccess = false });
            }
            var user = new IdentityUser
            {
                UserName = model.Username
            };

            var checkRoleExist = await _roleManager.RoleExistsAsync(model.Name);
            if (checkRoleExist == false)
            {
                return BadRequest(new UserResponse
                {
                    Message = "A role does not exist so it cannot create a user without a role",
                    IsSuccess = false
                });
            }
            var checkIsInRole = await _userManager.IsInRoleAsync(user, model.Role);
            if (checkIsInRole)
            {
                return Conflict("User already exists in this role");
            }


            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var addUserToRole = await _userManager.AddToRoleAsync(user, model.Role);
                if (addUserToRole.Succeeded)
                {
                    return Ok(new UserResponse
                    {
                        Message = $"Successfully created user {model.Username} and successfully added to role {model.Role}",
                        IsSuccess = true,
                    });
                }
            }
            return BadRequest();

        }

        // POST api/user/login
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] DtoUserLogin model)
        {
            IdentityUser user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                var signInRes = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (signInRes.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    var strToken = _tokenGenerator.AccessJWToken(user, roles[0]).ToString();

                    return Ok(new UserResponse
                    {
                        Message = "Successfully logged",
                        IsSuccess = true,
                        JwtResponseToken = strToken
                    });
                }
                return BadRequest(new UserResponse { Message = "Insufficient information", IsSuccess = false });
            }
            return BadRequest(new UserResponse { Message = "Failed login, wrong username or password", IsSuccess = false });
        }

        // POST api/user/logout
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new UserResponse { Message = "Succesfully Logedout", IsSuccess = true });
        }

        // GET api/user
        [HttpGet("")]
        [Authorize(Roles = "manager, admin")]
        public ActionResult<IEnumerable<IdentityUser>> GetUsers()
        {
            return _context.Users.ToList();
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public ActionResult<string> GetstringById(int id)
        {
            return null;
        }

        // POST api/user
        [HttpPost("")]
        public void Poststring(string value)
        {
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public void Putstring(int id, string value)
        {
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void DeletestringById(int id)
        {
        }
    }
}