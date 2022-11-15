namespace CarDealer.Web.Models.Customers
{
    using CarDealer.Services.Models;
    using System.Collections.Generic;
    using Services.Models.Customers;

    public class AllCustomersModel
    {
        public IEnumerable<CustomerModel> Customers { get; set; }

        public OrderDirection OrderDirection { get; set; }
    }
}
