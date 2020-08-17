using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace userApi.Models
{
    public class UserContext : IdentityDbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }

    }
}