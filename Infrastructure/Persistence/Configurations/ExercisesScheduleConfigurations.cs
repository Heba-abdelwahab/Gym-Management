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
    internal class ExercisesScheduleConfigurations : IEntityTypeConfiguration<ExercisesSchedule>
    {
        public void Configure(EntityTypeBuilder<ExercisesSchedule> builder)
        {
            builder.OwnsOne(ms => ms.schedule);
        }
    }
}
