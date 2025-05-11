// GymTracker/Services/Authentication/IAuthenticationService.cs
using System.Threading.Tasks;
using GymTracker.Data.Models;
using GymTracker.Models;

namespace GymTracker.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task SignInUserAsync(User user);
    }
}
