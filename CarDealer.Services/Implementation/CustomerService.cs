namespace CarDealer.Services.Implementation
{
    using Data;
    using Models;
    using Models.Cars;
    using Models.Customers;
    using Models.Sales;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> Ordered(OrderDirection order)
        {
            var custormersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    custormersQuery = custormersQuery.OrderBy(c => c.BirthDate).ThenBy(c => c.Name);
                    break;
                case OrderDirection.Descending:
                    custormersQuery = custormersQuery.OrderByDescending(c => c.BirthDate).ThenBy(c => c.Name);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid order direction: {order}.");
            }

            return custormersQuery
                .Select(c => new CustomerModel
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                }).ToList();
        }

        public CustomerTotalSalesModel TotalSalesById(int id)
        {
            return this.db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerTotalSalesModel
                {
                    Name = c.Name,
                    IsYoungDriver = c.IsYoungDriver,
                    BoughtCars = c.Sales.Select(s => new SaleModel
                    {
                        Price = s.Car.Parts.Sum(p => p.Part.Price),
                        Discount = s.Discount
                    })
                })
                .FirstOrDefault();
        }
    }
}
