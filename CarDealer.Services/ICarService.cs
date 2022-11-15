using CarDealer.Services.Models.Cars;
using CarDealer.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services
{
    public interface ICarService
    {
        IEnumerable<CarModel> ByMakeAtService(string make);

        IEnumerable<CarWithPartsModel> WithParts();
        void Create(string make, string model, long travelledDistance);
    }
}
