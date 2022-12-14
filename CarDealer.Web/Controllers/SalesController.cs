namespace CarDealer.Web.Controllers
{
    using CarDealer.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    
    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService sales;

        public SalesController(ISaleService sales)
        {
            this.sales = sales;
        }

        [Route("")]
        public IActionResult All()
        {
            return View(this.sales.All());
        }

        [Route("{id}")]
        public IActionResult Details(int id)
        {
           return this.ViewOrNotFound(this.sales.Details(id));
        }   
    }
}
