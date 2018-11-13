using System.Collections.Generic;
using System.Linq;

namespace TutorStrikeForce.Models
{
    public class SalesRep
    {
        public int SalesRepId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public IEnumerable<Sale> Sales { get; set; }
        public IEnumerable<Sale> GetSalesByDayOfMonth(int dayOfMonth)
        {
            return Sales.Where(sale => sale.SoldDate.Day == dayOfMonth);
        }

        public decimal GetSoldHoursByDayOfMonth(int dayOfMonth)
        {
            return GetSalesByDayOfMonth(dayOfMonth).Sum(sale => sale.Hours);
        }
    }
}
