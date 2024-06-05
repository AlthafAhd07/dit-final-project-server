using SkillsInternationalServer.Models;
using SkillsInternationalServer.Repositories;

namespace SkillsInternationalServer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;

        public AuthService(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> LoginUser(string username, string password)
        {
            return await _repository.LoginUser(username, password);
        }
    }
}
