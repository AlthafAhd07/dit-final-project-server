using Microsoft.EntityFrameworkCore;
using SkillsInternationalServer.Data;
using SkillsInternationalServer.Models;

namespace SkillsInternationalServer.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext context;

        public AuthRepository(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<User?> LoginUser(string username , string password) {
            User? user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);


            if (user == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null; 
            }

            return user;
        }
    }
}
