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
    internal class TraineeConfigurations : IEntityTypeConfiguration<Trainee>
    {
        public void Configure(EntityTypeBuilder<Trainee> builder)
        {
            builder.OwnsOne(trainee => trainee.Address, address =>
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
            builder.HasOne(t => t.Coach)
                .WithMany(c => c.Trainees)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t => t.Membership)
                .WithMany(m => m.Trainees)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
