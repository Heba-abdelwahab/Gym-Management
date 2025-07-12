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
    internal class GymFeatureConfigurations : IEntityTypeConfiguration<GymFeature>
    {
        public void Configure(EntityTypeBuilder<GymFeature> builder)
        {
            builder.OwnsOne(gf => gf.Image);
        }
    }
}
