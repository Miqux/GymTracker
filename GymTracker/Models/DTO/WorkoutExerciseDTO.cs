// File: GymTracker/Models/Command/WorkoutExerciseDTO.cs
using System.ComponentModel.DataAnnotations;

namespace GymTracker.Models.Command
{
    public class WorkoutExerciseDTO
    {
        [Required]
        public int ExerciseId { get; set; }

        [Required(ErrorMessage = "Liczba serii jest wymagana.")]
        public int Sets { get; set; }

        [Required(ErrorMessage = "Liczba powtórzeñ jest wymagana.")]
        public int Reps { get; set; }

        // Opcjonalnie
        public decimal? Weight { get; set; }
    }
}
