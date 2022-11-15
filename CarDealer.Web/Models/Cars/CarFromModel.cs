using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealer.Web.Models.Cars
{
    public class CarFromModel
    {
        [Required]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Display(Name = "Travelled Distance")]
        [Range(0, long.MaxValue, ErrorMessage = "{2} must be positive number.")]
        public long TravelledDistance { get; set; }
    }
}
