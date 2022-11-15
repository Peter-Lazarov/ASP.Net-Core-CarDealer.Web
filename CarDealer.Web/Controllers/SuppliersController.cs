using CarDealer.Services;
using CarDealer.Services.Models;
using CarDealer.Web.Models.Suppliers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealer.Web.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISupplierService suppliers;

        public SuppliersController(ISupplierService suppliers)
        {
            this.suppliers = suppliers;
        }

        public IActionResult Local()
        {
            return View("Suppliers", this.GetSuppliersModel(false));
        }

        public IActionResult Importers()
            => View("Suppliers", this.GetSuppliersModel(true));

        private SuppliersModel GetSuppliersModel(bool importers)
        {
            var type = importers ? "Importer" : "Local";

            var suppliers = this.suppliers.AllListings(importers);

            return new SuppliersModel
            {
                Type = type,
                Suppliers = suppliers
            };
        }
    }
}
