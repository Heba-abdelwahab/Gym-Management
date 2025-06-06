using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    internal class WorkDaysConfiguration : IEntityTypeConfiguration<WorkDay>
    {
        public void Configure(EntityTypeBuilder<WorkDay> builder)
        {
            builder.Property(wd=>wd.Day)
                .HasConversion(wd=>wd.ToString(), wdr => Enum.Parse<DayOfWeek>(wdr));

        }
    }
}
