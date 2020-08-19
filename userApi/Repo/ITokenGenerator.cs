using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace userApi.Repo
{
    public interface ITokenGenerator
    {
        string AccessJWToken(IdentityUser user, string role);
    }
}