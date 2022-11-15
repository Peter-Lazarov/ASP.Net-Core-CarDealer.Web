namespace CarDealer.Services.Implementation
{
    using CarDealer.Services.Models.Cars;
    using Data;
    using Models.Sales;
    using System.Collections.Generic;
    using System.Linq;

    public class SaleService : ISaleService
    {
        private readonly CarDealerDbContext db;

        public SaleService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleListModel> All()
            => this.db
            .Sales
            .Select(s => new SaleListModel
            {
                Id = s.Id,
                CustomerName = s.Customer.Name,
                Price = s.Car.Parts.Sum(p => p.Part.Price),
                IsYoungDriver = s.Customer.IsYoungDriver,
                Discount = s.Discount
            })
            .ToList();

        public SaleDetailsModel Details(int id)
        {
           return this.db
                .Sales
                .Where(s => s.Id == id)
                .Select(s => new SaleDetailsModel
                {
                    Id = s.Id,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.Parts.Sum(p => p.Part.Price),
                    IsYoungDriver = s.Customer.IsYoungDriver,
                    Discount = s.Discount,
                    Car = new CarModel
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    }
                })
                .FirstOrDefault();
        }
    }
}
