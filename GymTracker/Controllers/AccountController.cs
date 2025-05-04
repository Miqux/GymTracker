using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GymTracker.Models;
using GymTracker.Services;

namespace GymTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        // GET: /Account/Register
        [HttpGet("/Account/Register")]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost("/Account/Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                // Przy niepoprawnych danych zwracamy widok z b³êdami
                return View(registerDto);
            }

            try
            {
                // Mapowanie DTO do Command Model
                var command = new RegisterUserCommand
                {
                    Email = registerDto.Email,
                    Password = registerDto.Password
                };

                // Wywo³anie logiki rejestracji
                var result = await _accountService.RegisterUserAsync(command);
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.ErrorMessage);
                    TempData["ErrorMessage"] = result.ErrorMessage;
                    TempData["IsSuccess"] = false;
                    return View(registerDto);
                }

                // Rejestracja zakoñczona sukcesem – ustawienie komunikatu i flagi
                TempData["SuccessMessage"] = "Rejestracja zakoñczona pomyœlnie. Kliknij przycisk, aby przejœæ na stronê g³ówn¹.";
                TempData["IsSuccess"] = true;
                return View(registerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration");
                ModelState.AddModelError("", "Wyst¹pi³ b³¹d podczas rejestracji.");
                TempData["ErrorMessage"] = "Wyst¹pi³ b³¹d podczas rejestracji.";
                TempData["IsSuccess"] = false;
                return View(registerDto);
            }
        }
    }
}
