using CarDealer.Services.Models;
using CarDealer.Services.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealer.Web.Models.Suppliers
{
    public class SuppliersModel
    {
        public string Type { get; set; }

        public IEnumerable<SupplierListingModel> Suppliers { get; set; }
    }
}
