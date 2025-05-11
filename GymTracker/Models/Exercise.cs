using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTracker.Data.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string MuscleGroup { get; set; }

        [Required, MaxLength(20)]
        public string DifficultyLevel { get; set; }

        public string Description { get; set; }

        public bool IsBlocked { get; set; }

        public User User { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
