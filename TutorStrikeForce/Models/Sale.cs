using System;

namespace TutorStrikeForce.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int SalesRepId { get; set; }
        public int ClientId { get; set; }
        public decimal Hours { get; set; }
        public decimal PercentageOfSale { get; set; }
        public DateTime SoldDate { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
