using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace Persistence
{
    public partial class GymDbContext:IdentityDbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext>options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly (typeof(GymDbContext).Assembly);
            base.OnModelCreating(builder);
            OnModelCreatingPartial(builder);

        }
        public partial void OnModelCreatingPartial(ModelBuilder builder);

        public DbSet<Admin> Admins { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<ExercisesSchedule> ExercisesSchedules { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<GymOwner> GymOwners { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealSchedule> MealSchedules { get; set; }
        public DbSet<Membership> Memberships { get; set; }
       // public DbSet<Muscle> Muscles { get; set; }
        public DbSet<MuscleExerices> MuscleExercises { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }


    }
   
}
