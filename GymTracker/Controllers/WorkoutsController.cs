using GymTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Globalization;
using GymTracker.Models.DTO;
using GymTracker.Models.Command;

namespace GymTracker.Controllers
{
    [Authorize]
    public class WorkoutsController : Controller
    {
        private readonly IWorkoutService _workoutService;
        private readonly IExerciseService _exerciseService;

        public WorkoutsController(IWorkoutService workoutService, IExerciseService exerciseService)
        {
            _workoutService = workoutService;
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<IActionResult> History(int? page, int? pageSize, string? dateFrom, string? dateTo)
        {
            // Ustawienia domyślne dla paginacji
            int currentPage = page.HasValue && page.Value > 0 ? page.Value : 1;
            int currentPageSize = pageSize.HasValue && pageSize.Value > 0 ? pageSize.Value : 10;

            // Parsowanie opcjonalnych parametrów daty
            DateTime? dtFrom = null;
            DateTime? dtTo = null;
            if (!string.IsNullOrEmpty(dateFrom) && DateTime.TryParseExact(dateFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedFrom))
            {
                dtFrom = parsedFrom;
            }
            if (!string.IsNullOrEmpty(dateTo) && DateTime.TryParseExact(dateTo, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTo))
            {
                dtTo = parsedTo;
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                var history = await _workoutService.GetUserWorkoutHistoryAsync(userId, currentPage, currentPageSize, dtFrom, dtTo);
                return View(history);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<WorkoutHistoryDTO>());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            // Pobranie dostępnych ćwiczeń (tylko niezablokowanych)
            var availableExercises = await _exerciseService.GetUserExercisesAsync(userId);
            // Filtrowanie tylko niezablokowanych ćwiczeń
            ViewBag.AvailableExercises = availableExercises.Where(e => !e.IsBlocked).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutCreateCommand command)
        {
            if (!ModelState.IsValid)
            {
                // Przy ponownym wyświetleniu formularza upewnij się, że dostępne ćwiczenia są pobrane
                var userIdClaim2 = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim2 != null && int.TryParse(userIdClaim2.Value, out int uid))
                {
                    var availableExercises = await _exerciseService.GetUserExercisesAsync(uid);
                    ViewBag.AvailableExercises = availableExercises.Where(e => !e.IsBlocked).ToList();
                }
                return View(command);
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                await _workoutService.CreateWorkoutAsync(userId, command);
                TempData["SuccessMessage"] = "Trening został dodany pomyślnie.";
                return RedirectToAction("History");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var availableExercises = await _exerciseService.GetUserExercisesAsync(userId);
                ViewBag.AvailableExercises = availableExercises.Where(e => !e.IsBlocked).ToList();
                return View(command);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                await _workoutService.DeleteWorkoutAsync(userId, id);
                TempData["SuccessMessage"] = "Trening został pomyślnie usunięty.";
                return RedirectToAction("History");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("History");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                var workoutDetails = await _workoutService.GetWorkoutDetailsAsync(userId, id);
                return View(workoutDetails);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("History");
            }
        }
    }
}