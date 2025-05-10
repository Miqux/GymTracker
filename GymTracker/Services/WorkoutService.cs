using GymTracker.Data;
using GymTracker.Data.Models;
using GymTracker.Models.Command;
using GymTracker.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly GymTrackerContext _context;

        public WorkoutService(GymTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<WorkoutHistoryDTO>> GetUserWorkoutHistoryAsync(int userId, int page, int pageSize, DateTime? dateFrom, DateTime? dateTo)
        {
            var query = _context.Workouts
                .AsNoTracking()
                .Where(w => w.UserId == userId);

            if (dateFrom.HasValue)
            {
                query = query.Where(w => w.Date >= dateFrom.Value);
            }

            if (dateTo.HasValue)
            {
                query = query.Where(w => w.Date <= dateTo.Value);
            }

            // Sortowanie malejąco po dacie
            query = query.OrderByDescending(w => w.Date);

            // Używamy paginacji
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var result = await query.Select(w => new WorkoutHistoryDTO
            {
                Id = w.Id,
                Date = w.Date,
                Notes = w.Notes
            }).ToListAsync();

            return result;
        }
        public async Task CreateWorkoutAsync(int userId, WorkoutCreateCommand command)
        {
            // Walidacja - upewnij się, że lista ćwiczeń nie jest pusta
            if (command.Exercises == null || !command.Exercises.Any())
            {
                throw new Exception("Lista ćwiczeń nie może być pusta.");
            }

            // Sprawdzenie, czy każde ćwiczenie jest dostępne (nie jest zablokowane)
            foreach (var exerciseDto in command.Exercises)
            {
                var exercise = await _context.Exercises
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == exerciseDto.ExerciseId && e.UserId == userId);
                if (exercise == null)
                {
                    throw new Exception($"Ćwiczenie o identyfikatorze {exerciseDto.ExerciseId} nie zostało znalezione.");
                }
                if (exercise.IsBlocked)
                {
                    throw new Exception($"Ćwiczenie '{exercise.Name}' jest zablokowane i nie może być użyte.");
                }
            }

            // Tworzenie treningu
            var workout = new Workout
            {
                UserId = userId,
                Date = command.Date,
                Notes = command.Notes
            };

            // Dodanie treningu do kontekstu
            await _context.Workouts.AddAsync(workout);
            await _context.SaveChangesAsync();

            // Dodanie powiązanych ćwiczeń do treningu
            foreach (var exerciseDto in command.Exercises)
            {
                var workoutExercise = new WorkoutExercise
                {
                    WorkoutId = workout.Id,
                    ExerciseId = exerciseDto.ExerciseId,
                    Sets = exerciseDto.Sets,
                    Reps = exerciseDto.Reps,
                    Weight = exerciseDto.Weight
                };
                await _context.WorkoutExercises.AddAsync(workoutExercise);
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteWorkoutAsync(int userId, int workoutId)
        {
            var workout = await _context.Workouts
                .FirstOrDefaultAsync(w => w.Id == workoutId && w.UserId == userId);

            if (workout == null)
            {
                throw new Exception("Trening nie został znaleziony lub nie należy do bieżącego użytkownika.");
            }

            // Usuwamy powiązane rekordy ćwiczeń treningu (opcjonalnie, zależy od konfiguracji kaskadowej)
            var workoutExercises = await _context.WorkoutExercises.Where(we => we.WorkoutId == workoutId).ToListAsync();
            _context.WorkoutExercises.RemoveRange(workoutExercises);

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
        }
        public async Task<WorkoutDetailsDTO> GetWorkoutDetailsAsync(int userId, int workoutId)
        {
            var workout = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                    .ThenInclude(we => we.Exercise)
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == workoutId && w.UserId == userId);

            if (workout == null)
            {
                throw new Exception("Trening nie został znaleziony lub nie należy do bieżącego użytkownika.");
            }

            var detailsDto = new WorkoutDetailsDTO
            {
                Id = workout.Id,
                Date = workout.Date,
                Notes = workout.Notes,
                Exercises = workout.WorkoutExercises.Select(we => new WorkoutExerciseDetailsDTO
                {
                    ExerciseName = we.Exercise.Name,
                    Sets = we.Sets,
                    Reps = we.Reps,
                    Weight = we.Weight
                }).ToList()
            };

            return detailsDto;
        }
    }
}
