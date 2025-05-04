using System.Threading.Tasks;
using GymTracker.Models;

namespace GymTracker.Services
{
    public interface IAccountService
    {
        Task<RegisterResult> RegisterUserAsync(RegisterUserCommand command);
    }

    public class RegisterResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
