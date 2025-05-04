using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymTracker.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required, MaxLength(255)]
        public string Email { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<Workout> Workouts { get; set; }
    }
}
