using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GymTracker.Data;
using GymTracker.Models;
using System.Security.Cryptography;
using System.Text;
using GymTracker.Data.Models;

namespace GymTracker.Services
{
    public class AccountService : IAccountService
    {
        private readonly GymTrackerContext _context;
        private readonly ILogger<AccountService> _logger;

        public AccountService(GymTrackerContext context, ILogger<AccountService> logger)
        {
            _context = context;
            _logger = logger;
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
