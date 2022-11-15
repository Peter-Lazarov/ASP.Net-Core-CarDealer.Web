using CarDealer.Services.Models.Cars;
using CarDealer.Services.Models.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services.Models.Cars
{
    public class CarWithPartsModel : CarModel
    {
        public IEnumerable<PartModel> Parts { get; set; }
    }
}
