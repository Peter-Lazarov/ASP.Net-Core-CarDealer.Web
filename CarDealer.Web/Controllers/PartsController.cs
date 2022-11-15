namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Parts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class PartsController : Controller
    {
        private const int PageSize = 25;

        private readonly IPartService parts;
        private readonly ISupplierService supplier;

        public PartsController(IPartService parts, ISupplierService supplier)
        {
            this.parts = parts;
            this.supplier = supplier;
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var part = this.parts.ById(id);

            if (part == null)
            {
                return NotFound();
            }

            return View(new PartFromModel
            {
                Name = part.Name,
                Price = part.Price,
                Quantity = part.Quantity,
                IsEdit = true
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, PartFromModel model)
        {
            if (!ModelState.IsValid)
            {
                model.IsEdit = true;
                return View(model);
            }

            this.parts.Edit(
                id,
                model.Price,
                model.Quantity);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            return View(id);
        }

        public IActionResult Destroy(int id)
        {
            this.parts.Delete(id);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All(int page = 1)
        {
            return View(new PartPageListingModel
            {
                Parts = this.parts.All(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.parts.Total() / (double)PageSize)
            });
        }

        //public IActionResult Create()
        //    => View(new PartFromModel
        //    {
        //        Suppliers = this.supplier.All().Select(
        //            s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        //            {
        //                Text = s.Name,
        //                Value = s.Id.ToString()
        //            })
        //    });

        [HttpGet]
        public IActionResult Create()
         => View(new PartFromModel
         {
             Suppliers = this.GetSupplierListItems()
         });

        [HttpPost]
        public IActionResult Create(PartFromModel model)
        {
            if (true) // supplier doesn't exist
            {
                ModelState.AddModelError(nameof(PartFromModel.SupplierId), "Invalid supplier.");
            }

            if (!ModelState.IsValid)
            {
                model.Suppliers = this.GetSupplierListItems();
                return View(model);
            }

            this.parts.Create(
                model.Name,
                model.Price,
                model.Quantity,
                model.SupplierId
                );

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<SelectListItem> GetSupplierListItems()
        {
            return this.supplier
                 .All()
                 .Select(s => new SelectListItem
                 {
                     Text = s.Name,
                     Value = s.Id.ToString()
                 });
        }


    }
}
