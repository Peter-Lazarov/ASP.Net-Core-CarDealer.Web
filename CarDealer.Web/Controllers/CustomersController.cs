namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Services.Models;
    using Models.Customers;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CarDealer.Web.Infrastructure.Extensions;

    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "ascending"
                ? OrderDirection.Ascending
                : OrderDirection.Descending;

            var customers = this.customers.Ordered(orderDirection);

            return View(new AllCustomersModel
            {
                Customers = customers,
                OrderDirection = orderDirection
            });
        }

        public IActionResult TotalSales(int id)
        {
            return this.ViewOrNotFound(this.customers.TotalSalesById(id));

            //var customerWithSales = this.customers.TotalSalesById(id);

            //if (customerWithSales == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    return View(customerWithSales);
            //}
        }
        //=> this.View(this.customers.TotalSalesById(id));
    }
}
