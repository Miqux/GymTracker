using GymTracker.Data.DTO;
using GymTracker.Models.Command;

namespace GymTracker.Services
{
    public interface IExerciseService
    {
        Task<List<ExerciseListDTO>> GetUserExercisesAsync(int userId);
        Task CreateExerciseAsync(int userId, ExerciseCreateCommand command);
        Task BlockExerciseAsync(int exerciseId, int userId);
        Task<ExerciseEditCommand> GetExerciseEditCommandAsync(int userId, int exerciseId);
        Task EditExerciseAsync(int userId, ExerciseEditCommand command);
    }
}
