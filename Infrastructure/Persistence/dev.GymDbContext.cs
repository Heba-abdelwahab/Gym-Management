using Domain.Entities;
using Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public partial class GymDbContext
    {
        public partial void OnModelCreatingPartial(ModelBuilder builder)
        {
            builder.Entity<Admin>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<Class>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<Coach>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<ExercisesSchedule>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<Feature>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<Gym>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<GymOwner>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<Meal>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<MealSchedule>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<Membership>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<MuscleExerices>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<Trainee>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<WorkDay>().HasQueryFilter(c => !c.IsDeleted);


            builder.Entity<Message>()
                  .HasOne(message => message.Recipient)
                  .WithMany(recipient => recipient.MessagesReceived)
                  .HasForeignKey(message => message.RecipientId)
                  .OnDelete(DeleteBehavior.Restrict);



            builder.Entity<Message>()
                    .HasOne(message => message.Sender)
                    .WithMany(Sender => Sender.MessagesSent)
                    .HasForeignKey(message => message.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
