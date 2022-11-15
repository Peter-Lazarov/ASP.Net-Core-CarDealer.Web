using CarDealer.Data;
using CarDealer.Data.Models;
using CarDealer.Services.Models.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealer.Services.Implementation
{
    public class PartService : IPartService
    {
        private readonly CarDealerDbContext db;

        public PartService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PartListingModel> All(int page = 1, int pageSize = 10)
        {
            return this.db
                .Parts
                .OrderByDescending(s => s.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PartListingModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierName = p.Supplier.Name
                })
                .ToList();
        }

        public void Create(string name, decimal price, int quantity, int supplierId)
        {
            var part = new Part
            {
                Name = name,
                Price = price,
                Quantity = quantity > 0 ? quantity : 1,
                SupplierId = supplierId
            };

            this.db.Add(part);
            this.db.SaveChanges();
        }

        public PartDetailsModel ById(int id)
        {
            var partListing = this.db
                .Parts
                .Where(p => p.Id == id)
                .Select(p => new PartDetailsModel
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                })
                .FirstOrDefault();

            return partListing;
        }

        public void Edit(int id, decimal price, int quantity)
        {
            var part = this.db.Parts.Find(id);

            if (part == null)
            {
                return;
            }

            part.Price = price;
            part.Quantity = quantity;

            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var part = this.db.Parts.Find(id);

            if (part == null)
            {
                return;
            }

            this.db.Parts.Remove(part);
            this.db.SaveChanges();
        }

        public int Total()
        {
            return this.db.Parts.Count();
        }
    }
}
