using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    internal class GymConfigurations : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.OwnsOne(p => p.Address, address =>
            {
                address.Property(o => o.City).HasColumnType("varchar(50)");
                address.Property(o => o.Street).HasColumnType("varchar(50)");
                address.Property(o => o.Country).HasColumnType("varchar(50)");
                address.OwnsOne(address=> address.Location, location =>
                {
                    location.Property(location => location.X).HasColumnType("float");
                    location.Property(location => location.Y).HasColumnType("float");
                });
            });

            builder.Property(g => g.Name).HasColumnType("varchar(50)");
            builder.Property(g => g.Phone).HasColumnType("varchar(50)");
            builder.Property(g => g.Description).HasColumnType("varchar(50)");

            builder.HasOne(g=>g.GymOwner)
                .WithMany(o=>o.Gyms)
                .HasForeignKey(g=>g.GymOwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(g=>g.GymFeatures)
                .WithOne(gf=>gf.Gym)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(g => g.GymCoaches)
                .WithOne(gc => gc.Gym)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(g => g.Memberships)
                .WithOne(m => m.Gym)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(g => g.Classes)
                .WithOne(c=> c.Gym)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(g => g.Trainees)
                .WithOne(t => t.Gym)
                .OnDelete(DeleteBehavior.NoAction);

            builder.OwnsOne(g => g.Media);
        }
    }
}
