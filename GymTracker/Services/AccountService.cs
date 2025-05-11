using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GymTracker.Data;
using GymTracker.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using GymTracker.Models.Command;
using GymTracker.Models.DTO;
using GymTracker.Services.Security;
using GymTracker.Services.Repositories;
using GymTracker.Services.Authentication;
using GymTracker.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace GymTracker.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AccountService> _logger;
        private readonly IPasswordHasher _passwordHasher;
        private readonly Authentication.IAuthenticationService _authenticationService;

        public AccountService(
            IUserRepository userRepository,
            ILogger<AccountService> logger,
            IPasswordHasher passwordHasher,
            Authentication.IAuthenticationService authenticationService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        public async Task<RegisterResult> RegisterUserAsync(RegisterUserCommand command)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.Password))
                {
                    return CreateFailedRegistration("Email i has³o s¹ wymagane.");
                }

                if (await _userRepository.UserExistsByEmailAsync(command.Email))
                {
                    return CreateFailedRegistration("U¿ytkownik o podanym adresie email ju¿ istnieje.");
                }

                var passwordHash = _passwordHasher.HashPassword(command.Password);

                User newUser = new User
                {
                    Email = command.Email,
                    PasswordHash = passwordHash
                };

                await _userRepository.AddUserAsync(newUser);

                return new RegisterResult { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "B³¹d podczas rejestracji u¿ytkownika");
                return CreateFailedRegistration("Wyst¹pi³ b³¹d podczas rejestracji.");
            }
        }

        public async Task<LoginResult> LoginUserAsync(LoginCommand command)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.Password))
                {
                    return CreateFailedLogin("Email i has³o s¹ wymagane.");
                }

                var user = await _userRepository.GetUserByEmailAsync(command.Email);

                if (user == null)
                {
                    return CreateFailedLogin("Nieprawid³owy email lub has³o.");
                }

                if (!_passwordHasher.VerifyPassword(command.Password, user.PasswordHash))
                {
                    return CreateFailedLogin("Nieprawid³owy email lub has³o.");
                }

                await _authenticationService.SignInUserAsync(user);

                return new LoginResult { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user login");
                return CreateFailedLogin("Wyst¹pi³ b³¹d podczas logowania.");
            }
        }

        private static RegisterResult CreateFailedRegistration(string errorMessage)
        {
            return new RegisterResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }

        private static LoginResult CreateFailedLogin(string errorMessage)
        {
            return new LoginResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
