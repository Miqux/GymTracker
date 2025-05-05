using System.Threading.Tasks;
using GymTracker.Models;
using GymTracker.Models.Command;
using GymTracker.Models.DTO;

namespace GymTracker.Services
{
    public interface IAccountService
    {
        Task<RegisterResult> RegisterUserAsync(RegisterUserCommand command);
        Task<LoginResult> LoginUserAsync(LoginCommand command);
    }

    public class RegisterResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
