using SkillsInternationalServer.Models;

namespace SkillsInternationalServer.Repositories
{
    public interface IAuthRepository
    {
        Task<User?> LoginUser(string username, string password);
    }
}
