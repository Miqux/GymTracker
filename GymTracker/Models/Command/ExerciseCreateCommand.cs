// File: GymTracker/Models/Command/ExerciseCreateCommand.cs
using System.ComponentModel.DataAnnotations;

namespace GymTracker.Models.Command
{
    public class ExerciseCreateCommand
    {
        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Nazwa musi mie� od 3 do 150 znak�w.")]
        public string Name { get; set; }

        [Required]
        public string MuscleGroup { get; set; }

        [Required]
        public string DifficultyLevel { get; set; }

        public string Description { get; set; }
    }
}
