namespace CarDealer.Services.Models.Customers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }
    }
}
