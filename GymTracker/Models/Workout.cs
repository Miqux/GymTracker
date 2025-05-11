using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymTracker.Data.Models
{
    public class Workout
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Notes { get; set; }

        public User User { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
