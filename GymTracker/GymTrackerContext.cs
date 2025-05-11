using GymTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Data
{
    public class GymTrackerContext : DbContext
    {
        public GymTrackerContext(DbContextOptions<GymTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PasswordHash).IsRequired();
            });

            // Exercises
            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(150);
                entity.Property(e => e.MuscleGroup)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(e => e.DifficultyLevel)
                      .IsRequired()
                      .HasMaxLength(20);
                entity.HasIndex(e => new { e.UserId, e.Name }).IsUnique();
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Exercises)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Workouts
            modelBuilder.Entity<Workout>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date)
                      .IsRequired();
                entity.Property(e => e.Notes);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Workouts)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // WorkoutExercises
            modelBuilder.Entity<WorkoutExercise>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Sets).IsRequired();
                entity.Property(e => e.Reps).IsRequired();
                entity.HasOne(we => we.Workout)
                      .WithMany(w => w.WorkoutExercises)
                      .HasForeignKey(we => we.WorkoutId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(we => we.Exercise)
                      .WithMany(e => e.WorkoutExercises)
                      .HasForeignKey(we => we.ExerciseId)
                      .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
