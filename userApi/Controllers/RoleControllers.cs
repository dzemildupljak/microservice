using System.Security.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using userApi.Models;
using userApi.Dto;

namespace userApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // GET api/role
        [HttpGet("")]
        public ActionResult<IEnumerable<IdentityRole>> GetRoles()
        {
            var roles = _context.Roles.ToList();
            return roles;
        }

        // GET api/role/5
        [HttpGet("{id}")]
        public ActionResult<IdentityRole> GetRoleById(string id)
        {
            var role = _context.Roles.Where(x => x.Id == id).FirstOrDefault();
            return role;
        }

        // POST api/role
        [HttpPost("")]
        public async Task<ActionResult> CreateRole([FromBody] DtoRole model)
        {
            var checkRoleExist = await _roleManager.RoleExistsAsync(model.RoleName);
            if (checkRoleExist == false)
            {
                var roleToAdd = new IdentityRole(model.RoleName);
                var roleRes = await _roleManager.CreateAsync(roleToAdd);
                if (roleRes.Succeeded)
                {
                    // return BadRequest("Role is not created");
                    return Ok($"Uspesno kreirana rola");
                }
                return BadRequest("Neuspesno kreirana rola!");
            }
            return Conflict("Rola vec postoji!");
        }

        // PUT api/role/5
        [HttpPut("{id}")]
        public void Putstring(int id, string value)
        {
        }
        // Ovo ne sme da se koristi al iz tehnickih razloga sam postavio da postoji
        // DELETE api/role/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleRoleById(string id)
        {
            var checkRoleExist = await _roleManager.FindByIdAsync(id);
            if (checkRoleExist == null)
            {
                var deleteRole = await _roleManager.DeleteAsync(checkRoleExist);
                if (deleteRole.Succeeded)
                {
                    return Ok($"Uspesno izbrisana rola");
                }
                return BadRequest("Neuspesno kreirana rola koja ne postoji!");
            }
            return Conflict("Rola ne postoji!");
        }
    }
}