using SkillsInternationalServer.Models;

namespace SkillsInternationalServer.Services
{
    public interface IAuthService
    {
        Task<User?> LoginUser(string username, string password);
    }
}
