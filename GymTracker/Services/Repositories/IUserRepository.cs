// GymTracker/Services/Repositories/IUserRepository.cs
using System.Threading.Tasks;
using GymTracker.Data.Models;
using GymTracker.Models;

namespace GymTracker.Services.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UserExistsByEmailAsync(string email);
        Task AddUserAsync(User user);
    }
}
