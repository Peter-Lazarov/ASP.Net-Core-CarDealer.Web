using CarDealer.Services.Models.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services
{
    public interface IPartService
    {
        IEnumerable<PartListingModel> All(int page = 1, int pageSize = 10);

        PartDetailsModel ById(int id);

        int Total();

        void Create(string name, decimal price, int quantity, int supplierId);

        void Edit(int id, decimal price, int quantity);

        void Delete(int id);
    }
}
