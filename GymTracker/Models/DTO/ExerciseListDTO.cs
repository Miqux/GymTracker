// File: GymTracker/Models/DTOs/ExerciseListDTO.cs
namespace GymTracker.Data.DTO
{
    public class ExerciseListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public string DifficultyLevel { get; set; }
        public string Description { get; set; }
        public bool IsBlocked { get; set; }
    }
}
