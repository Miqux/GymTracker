using GymTracker.Models.Command;
using GymTracker.Models.DTO;

namespace GymTracker.Services
{
    public interface IWorkoutService
    {
        Task<List<WorkoutHistoryDTO>> GetUserWorkoutHistoryAsync(int userId, int page, int pageSize, DateTime? dateFrom, DateTime? dateTo);
        Task CreateWorkoutAsync(int userId, WorkoutCreateCommand command);
        Task DeleteWorkoutAsync(int userId, int workoutId);
        Task<WorkoutDetailsDTO> GetWorkoutDetailsAsync(int userId, int workoutId);
    }
}
