﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class CoachConfigurations : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(o => o.City).HasColumnType("varchar(50)");
                address.Property(o => o.Street).HasColumnType("varchar(50)");
                address.Property(o => o.Country).HasColumnType("varchar(50)");

                address.OwnsOne(ad => ad.Location, location =>
                {
                    location.Property(location => location.X).HasColumnType("float");
                    location.Property(location => location.Y).HasColumnType("float");
                });
            });

            builder.OwnsOne(c => c.CV);
            builder.OwnsOne(c => c.Image);

            //builder.HasOne(c => c.AppUser)
            //    .WithOne()
            //    .HasForeignKey<Coach>(c => c.Id);
            builder.HasMany(c => c.GymCoaches)
                .WithOne(gc => gc.Coach)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.Trainees)
                .WithOne(t => t.Coach)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.Classes)
                .WithOne(c => c.Coach)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.ExercisesSchedules)
                .WithOne(es => es.Coach)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.MealSchedules)
                .WithOne(ms => ms.Coach)
                .OnDelete(DeleteBehavior.NoAction);

            #region I will make it later 

            //builder.Property(couch => couch.Specializations)
            //        .HasConversion(
            //    toDb => toDb.ToString()
            //    ,
            //    fromDb => Enum.Parse<CoachSpecialization>(fromDb)
            //    );

            #endregion




        }
    }
}
