using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using userApi.Dto;
using userApi.Models;
using userApi.DtoResponse;

namespace userApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserContext _context;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, UserContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        // POST api/user/register
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] DtoUserRegister model)
        {
            var user = new IdentityUser
            {
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new UserResponse { Message = "Succesfully created user", IsSuccess = true});
            }
            return BadRequest();
        }

        // POST api/user/login
        [HttpPost("login")]
        public async Task<ActionResult> login([FromBody] DtoUserLogin model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                var result =  await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok(new UserResponse { Message = "Succesfully logedin", IsSuccess = true});
                }
            }
            return BadRequest();
        }

        // POST api/user/logout
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new UserResponse { Message = "Succesfully Logedout", IsSuccess = true});
        }

        // GET api/user
        [HttpGet("")]
        public ActionResult<IEnumerable<IdentityUser>> Get()
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