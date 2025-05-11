// GymTracker/Services/Repositories/UserRepository.cs
using System.Threading.Tasks;
using GymTracker.Data;
using GymTracker.Data.Models;
using GymTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GymTrackerContext _context;

        public UserRepository(GymTrackerContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
