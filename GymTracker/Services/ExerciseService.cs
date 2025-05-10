// File: GymTracker/Services/ExerciseService.cs
using GymTracker.Data.Models;
using GymTracker.Data.DTO;
using Microsoft.EntityFrameworkCore;
using GymTracker.Data;
using GymTracker.Models.Command;

namespace GymTracker.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly GymTrackerContext _context;

        public ExerciseService(GymTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<ExerciseListDTO>> GetUserExercisesAsync(int userId)
        {
            return await _context.Exercises
                .AsNoTracking()
                .Where(e => e.UserId == userId)
                .Select(e => new ExerciseListDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup,
                    DifficultyLevel = e.DifficultyLevel,
                    Description = e.Description,
                    IsBlocked = e.IsBlocked
                })
                .ToListAsync();
        }

        public async Task CreateExerciseAsync(int userId, ExerciseCreateCommand command)
        {
            // Sprawdzenie unikalnoœci nazwy æwiczenia dla danego u¿ytkownika
            bool exists = await _context.Exercises
                .AsNoTracking()
                .AnyAsync(e => e.UserId == userId && e.Name.ToLower() == command.Name.ToLower());

            if (exists)
            {
                throw new Exception("Æwiczenie o podanej nazwie ju¿ istnieje.");
            }

            var exercise = new Exercise
            {
                UserId = userId,
                Name = command.Name,
                MuscleGroup = command.MuscleGroup,
                DifficultyLevel = command.DifficultyLevel,
                Description = command.Description,
                IsBlocked = false
            };

            await _context.Exercises.AddAsync(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task BlockExerciseAsync(int exerciseId, int userId)
        {
            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(e => e.Id == exerciseId && e.UserId == userId);

            if (exercise == null)
            {
                throw new Exception("Æwiczenie nie zosta³o znalezione lub nie nale¿y do bie¿¹cego u¿ytkownika.");
            }

            if (exercise.IsBlocked)
            {
                throw new Exception("Æwiczenie jest ju¿ zablokowane.");
            }

            exercise.IsBlocked = true;
            _context.Exercises.Update(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task<ExerciseEditCommand> GetExerciseEditCommandAsync(int userId, int exerciseId)
        {
            var exercise = await _context.Exercises
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == exerciseId && e.UserId == userId);

            if (exercise == null)
            {
                throw new Exception("Æwiczenie nie zosta³o znalezione lub nie nale¿y do bie¿¹cego u¿ytkownika.");
            }

            return new ExerciseEditCommand
            {
                Id = exercise.Id,
                Name = exercise.Name,
                MuscleGroup = exercise.MuscleGroup,
                DifficultyLevel = exercise.DifficultyLevel,
                Description = exercise.Description
            };
        }

        public async Task EditExerciseAsync(int userId, ExerciseEditCommand command)
        {
            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(e => e.Id == command.Id && e.UserId == userId);

            if (exercise == null)
            {
                throw new Exception("Æwiczenie nie zosta³o znalezione lub nie nale¿y do bie¿¹cego u¿ytkownika.");
            }

            // Opcjonalna walidacja unikalnoœci, je¿eli zmieniono nazwê
            if (!exercise.Name.Equals(command.Name, StringComparison.OrdinalIgnoreCase))
            {
                bool duplicateExists = await _context.Exercises
                    .AsNoTracking()
                    .AnyAsync(e => e.UserId == userId &&
                                   e.Id != command.Id &&
                                   e.Name.ToLower() == command.Name.ToLower());
                if (duplicateExists)
                {
                    throw new Exception("Æwiczenie o podanej nazwie ju¿ istnieje.");
                }
            }

            // Aktualizacja w³aœciwoœci æwiczenia
            exercise.Name = command.Name;
            exercise.MuscleGroup = command.MuscleGroup;
            exercise.DifficultyLevel = command.DifficultyLevel;
            exercise.Description = command.Description;

            _context.Exercises.Update(exercise);
            await _context.SaveChangesAsync();
        }
    }
}
