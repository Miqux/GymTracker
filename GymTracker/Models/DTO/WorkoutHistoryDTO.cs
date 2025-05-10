// File: GymTracker/Models/DTO/WorkoutHistoryDTO.cs
using System;

namespace GymTracker.Models.DTO
{
    public class WorkoutHistoryDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}
