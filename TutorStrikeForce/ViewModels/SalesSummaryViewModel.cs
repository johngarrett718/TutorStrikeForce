using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TutorStrikeForce.Models;

namespace TutorStrikeForce.ViewModels
{
    public class SalesSummaryViewModel
    {
        public SalesSummaryViewModel(List<MonthYearOption> options)
        {
            MonthYearOptions = new List<SelectListItem>();
            for(int i = 0; i < options.Count; i++)
            {
                MonthYearOptions.Add(new SelectListItem
                {
                    Text = options[i].DisplayDate(),
                    Value = options[i].Month.ToString()
                });
            }
        }

        public List<SalesRep> SalesReps { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public List<SelectListItem> MonthYearOptions { get; }
    }
}
