using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using userApi.Models;

namespace userApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }

        // GET api/user
        [HttpGet("")]
        public ActionResult<IEnumerable<User>> Getstrings()
        {
            var users = new List<User>();
            var user1 = new User{ Username = "admin", Password = "admin12"};
            users.Add(user1);
            var user2 = new User{ Username = "dzemil", Password = "12345"};
            users.Add(user2);
            return users;
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