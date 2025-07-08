using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record ExGymFeatureDto
    (
        [Required]
        string Name ,
        [Required]
        string Description,
        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage ="Cost must be greater than zero.")]
        decimal Cost
    );
}
