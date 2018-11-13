using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TutorStrikeForce.ViewModels
{
    public class SaleEditModel
    {
        public List<SelectListItem> Clients { get; set; }

        public List<SelectListItem> SalesReps { get; set; }

        public ClientEditModel Client { get; set; }

        [Required]
        [Display(Name = "Opener Sales Rep")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Sales Rep")]
        public int OpenerSalesRepId { get; set; }

        [Required]
        [Display(Name = "Closer Sales Rep")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Sales Rep")]
        public int CloserSalesRepId { get; set; }

        [Required]
        [Range(0.0, 1000.0)]
        public decimal Hours { get; set; }

        [Required]
        [Display(Name = "Percentage Of Sale")]
        [Range(0.0, 100.0)]
        public decimal PercentageOfSale { get; set; }

        [Required]
        [Display(Name = "Sold Date")]
        [DataType(DataType.Date)]
        public DateTime SoldDate { get; set; } = DateTime.Today;
    }
}
