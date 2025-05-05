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
                // Zwracamy widok z b��dami walidacji
                return View(loginDto);
            }

            // Mapowanie DTO na Command Model
            var command = new LoginCommand
            {
                Email = loginDto.Email,
                Password = loginDto.Password
            };

            // Wywo�anie logiki logowania
            var result = await _accountService.LoginUserAsync(command);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(loginDto);
            }

            TempData["SuccessMessage"] = "Pomy�lnie zalogowano.";

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
                // Przy niepoprawnych danych zwracamy widok z b��dami
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

                // Wywo�anie logiki rejestracji
                var result = await _accountService.RegisterUserAsync(command);
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.ErrorMessage);
                    TempData["ErrorMessage"] = result.ErrorMessage;
                    TempData["IsSuccess"] = false;
                    return View(registerDto);
                }

                // Rejestracja zako�czona sukcesem � ustawienie komunikatu i flagi
                TempData["SuccessMessage"] = "Rejestracja zako�czona pomy�lnie. Kliknij przycisk, aby przej�� na stron� g��wn�.";
                TempData["IsSuccess"] = true;
                return View(registerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration");
                ModelState.AddModelError("", "Wyst�pi� b��d podczas rejestracji.");
                TempData["ErrorMessage"] = "Wyst�pi� b��d podczas rejestracji.";
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
