using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GymTracker.Models;
using GymTracker.Services;
using GymTracker.Models.Command;
using GymTracker.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

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

        // GET: /Account/Login
        [HttpGet("/Account/Login")]
        public IActionResult Login()
        {
            TempData.Remove("SuccessMessage");
            return View();
        }

        // POST: /Account/Login
        [HttpPost("/Account/Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                // Zwracamy widok z b³êdami walidacji
                return View(loginDto);
            }

            // Mapowanie DTO na Command Model
            var command = new LoginCommand
            {
                Email = loginDto.Email,
                Password = loginDto.Password
            };

            // Wywo³anie logiki logowania
            var result = await _accountService.LoginUserAsync(command);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(loginDto);
            }

            TempData["SuccessMessage"] = "Pomyœlnie zalogowano.";

            return RedirectToAction("Index", "Home");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
