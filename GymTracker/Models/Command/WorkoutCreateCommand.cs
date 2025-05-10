// File: GymTracker/Models/Command/WorkoutCreateCommand.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymTracker.Models.Command
{
    public class WorkoutCreateCommand
    {
        [Required(ErrorMessage = "Data treningu jest wymagana.")]
        public DateTime Date { get; set; }

        public string Notes { get; set; }

        [Required(ErrorMessage = "Lista æwiczeñ nie mo¿e byæ pusta.")]
        public List<WorkoutExerciseDTO> Exercises { get; set; }
    }
}
