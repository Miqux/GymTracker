// File: GymTracker/Models/Command/ExerciseBlockCommand.cs
using System.ComponentModel.DataAnnotations;

namespace GymTracker.Models.Command
{
    public class ExerciseBlockCommand
    {
        [Required]
        public int Id { get; set; }
    }
}
