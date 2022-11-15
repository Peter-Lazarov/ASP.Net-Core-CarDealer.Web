using CarDealer.Services.Models.Suppliers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealer.Web.Models.Parts
{
    public class PartFromModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive number.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        public IEnumerable<SelectListItem> Suppliers{ get; set; }

        public bool IsEdit { get; set; }

    }
}
