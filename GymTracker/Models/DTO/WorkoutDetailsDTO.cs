// File: GymTracker/Models/DTO/WorkoutDetailsDTO.cs
using System;
using System.Collections.Generic;

namespace GymTracker.Models.DTO
{
    public class WorkoutDetailsDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<WorkoutExerciseDetailsDTO> Exercises { get; set; }
        public string Notes { get; set; }
    }

    public class WorkoutExerciseDetailsDTO
    {
        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal? Weight { get; set; }
    }
}
