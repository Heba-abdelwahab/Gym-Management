using Domain.Entities;
using Domain.Entities.Chat;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Persistence
{
    public partial class GymDbContext : IdentityDbContext<AppUser>
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);
            base.OnModelCreating(builder);
            OnModelCreatingPartial(builder);

        }
        public partial void OnModelCreatingPartial(ModelBuilder builder);

        public DbSet<Class> Classes { get; set; }
        public DbSet<ExercisesSchedule> ExercisesSchedules { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealSchedule> MealSchedules { get; set; }
        public DbSet<Membership> Memberships { get; set; }
       // public DbSet<Muscle> Muscles { get; set; }
        public DbSet<ScheduledExercise> ScheduledExercises { get; set; }
        // public DbSet<Muscle> Muscles { get; set; }
        public DbSet<MuscleExerices> MuscleExercises { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }



        #region Users

        public DbSet<GymOwner> GymOwners { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Trainee> Trainees { get; set; }

        #endregion

        //public new DbSet<AppUser> Users { get; set; } // i will change it later 

        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Connection> Connections => Set<Connection>();

        public DbSet<Muscle> Muscles { get; set; }

    }

}
