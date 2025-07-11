using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record GymGetDto
    {
        public AddressDto Address { get; set; }
        public GymType GymType { get; set; }
        public string? GymTypeValue { get; set; }
        public string MediaUrl { get; set; }
        public List<string> GymImagesUrl { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public IEnumerable<GymFeatureDto>?GymFeatures { get; set; }
    }
}
