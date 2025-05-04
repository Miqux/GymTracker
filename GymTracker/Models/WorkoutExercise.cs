using System.ComponentModel.DataAnnotations;

namespace GymTracker.Data.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        
        [Required]
        public int WorkoutId { get; set; }
        
        [Required]
        public int ExerciseId { get; set; }
        
        [Required]
        public int Sets { get; set; }
        
        [Required]
        public int Reps { get; set; }
        
        public decimal? Weight { get; set; }

        public Workout Workout { get; set; }
        public Exercise Exercise { get; set; }
    }
}
