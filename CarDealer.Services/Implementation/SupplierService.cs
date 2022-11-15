using CarDealer.Data;
using CarDealer.Services.Models;
using CarDealer.Services.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealer.Services.Implementation
{
    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierListingModel> AllListings(bool isImporter)
            => this.db
            .Suppliers
            .Where(s => s.IsImporter == isImporter)
            .Select(s => new SupplierListingModel
            {
                Id = s.Id,
                Name = s.Name,
                TotalParts = s.Parts.Count
            })
            .ToList();

        public IEnumerable<SupplierModel> All()
        {
            return this.db
                .Suppliers
                .OrderBy(s => s.Name)
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToList();
        }
    }
}
