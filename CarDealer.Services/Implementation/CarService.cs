namespace CarDealer.Services.Implementation
{
    using Data;
    using CarDealer.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using CarDealer.Services.Models.Cars;
    using CarDealer.Services.Models.Parts;
    using CarDealer.Data.Models;

    public class CarService : ICarService
    {
        public readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CarModel> ByMakeAtService (string make)
            => this.db
                .Cars
                .Where(c => c.Make.ToLower() == make.ToLower())
                .OrderBy(c => c.Model)
                .ThenBy(c => c.TravelledDistance)
                .Select(c => new CarModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();

        public IEnumerable<CarWithPartsModel> WithParts()
            => this.db
                .Cars
                .Select(c => new CarWithPartsModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.Parts.Select(p => new PartModel
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                })
                .ToList();

        public void Create(string make, string model, long travelledDistance)
        {
            var car = new Car
            {
                Make = make,
                Model = model,
                TravelledDistance = travelledDistance
            };

            this.db.Add(car);
            this.db.SaveChanges();
        }
    }
}
