using CarDealer.Services;
using CarDealer.Web.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealer.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService cars;

        public CarsController(ICarService cars)
        {
            this.cars = cars;
        }

        [HttpGet]
        [Route("cars/" + nameof(Create))]
        public IActionResult Create() => View();

        [HttpPost]
        [Route("cars/" + nameof(Create))]
        public IActionResult Create(CarFromModel carModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carModel);
            }

            this.cars.Create(
                carModel.Make,
                carModel.Model,
                carModel.TravelledDistance);

            return RedirectToAction(nameof(Parts));
        }

        //[Route("cars/{make}")]
        public IActionResult ByMake(string make)
        {
            var cars = this.cars.ByMakeAtService(make);

            return View(new CarsByMakeModel
            {
                Make = make,
                Cars = cars
            });
        }

        public IActionResult Parts()
            => View(this.cars.WithParts());
    }
}
