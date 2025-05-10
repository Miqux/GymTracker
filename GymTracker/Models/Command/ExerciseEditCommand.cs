// File: GymTracker/Models/Command/ExerciseEditCommand.cs
using System.ComponentModel.DataAnnotations;

namespace GymTracker.Models.Command
{
    public class ExerciseEditCommand
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Nazwa musi mieæ od 3 do 150 znaków.")]
        public string Name { get; set; }

        [Required]
        public string MuscleGroup { get; set; }

        [Required]
        public string DifficultyLevel { get; set; }

        public string Description { get; set; }
    }
}
