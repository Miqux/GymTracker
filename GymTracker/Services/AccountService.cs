using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GymTracker.Data;
using GymTracker.Models;
using System.Security.Cryptography;
using System.Text;
using GymTracker.Data.Models;
using GymTracker.Models.Command;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using GymTracker.Models.DTO;

namespace GymTracker.Services
{
    public class AccountService : IAccountService
    {
        private readonly GymTrackerContext _context;
        private readonly ILogger<AccountService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(GymTrackerContext context, ILogger<AccountService> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RegisterResult> RegisterUserAsync(RegisterUserCommand command)
        {
            try
            {
                // Sprawdzenie czy u¿ytkownik o podanym adresie email ju¿ istnieje
                var existingUser = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == command.Email.ToLower());

                if (existingUser != null)
                {
                    return new RegisterResult
                    {
                        Success = false,
                        ErrorMessage = "U¿ytkownik o podanym adresie email ju¿ istnieje."
                    };
                }

                // Hashowanie has³a
                var passwordHash = HashPassword(command.Password);

                // Tworzenie nowego u¿ytkownika i zapis do bazy
                var newUser = new User
                {
                    Email = command.Email,
                    PasswordHash = passwordHash
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                return new RegisterResult { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "B³¹d podczas rejestracji u¿ytkownika");
                return new RegisterResult
                {
                    Success = false,
                    ErrorMessage = "Wyst¹pi³ b³¹d podczas rejestracji."
                };
            }
        }
        public async Task<LoginResult> LoginUserAsync(LoginCommand command)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == command.Email.ToLower());

                if (user == null)
                {
                    return new LoginResult { Success = false, ErrorMessage = "Nieprawid³owy email lub has³o." };
                }

                var attemptedHash = HashPassword(command.Password);
                if (user.PasswordHash != attemptedHash)
                {
                    return new LoginResult { Success = false, ErrorMessage = "Nieprawid³owy email lub has³o." };
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
                var identity = new ClaimsIdentity(claims, "login");
                var principal = new ClaimsPrincipal(identity);

                // Logowanie u¿ytkownika przy u¿yciu mechanizmu Cookie Authentication
                await _httpContextAccessor.HttpContext.SignInAsync("Cookies", principal);

                // W tym scenariuszu metoda zwraca true (poprawne logowanie)
                return new LoginResult { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user login");
                return new LoginResult { Success = false, ErrorMessage = "Wyst¹pi³ b³¹d podczas logowania." };
            }
        }

        private string HashPassword(string password)
        {
            // Prosty przyk³ad haszowania (w produkcji u¿yj silniejszego mechanizmu)
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
