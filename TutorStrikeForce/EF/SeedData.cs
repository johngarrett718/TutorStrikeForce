using System;
using System.Collections.Generic;
using System.Linq;
using TutorStrikeForce.Models;

namespace TutorStrikeForce.EF
{
    public static class SeedData
    {
        public static SalesRep[] SeedSalesReps()
        {
            return new SalesRep[]
            {
                new SalesRep
                {
                    SalesRepId = 1,
                    FirstName = "John",
                    LastName = "Garrett",
                    IsDeleted = false
                },
                new SalesRep
                {
                    SalesRepId = 2,
                    FirstName = "Nick",
                    LastName = "Nelson",
                    IsDeleted = false
                },
                new SalesRep
                {
                    SalesRepId = 3,
                    FirstName = "Natalie",
                    LastName = "Benjamin",
                    IsDeleted = false
                }
            };
        }

        public static Client[] SeedClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = 1,
                    FirstName = "Brad",
                    LastName = "Pitt",
                    Email = "brad.pitt@gmail.com",
                    City = "Minneapolis",
                    Hours = 50.00m,
                    IsDeleted = false
                },
                new Client
                {
                    ClientId = 2,
                    FirstName = "George",
                    LastName = "Clooney",
                    Email = "george.c@gmail.com",
                    City = "St. Paul",
                    Hours = 55.00m,
                    IsDeleted = false
                },
                new Client
                {
                    ClientId = 3,
                    FirstName = "Bill",
                    LastName = "Gates",
                    Email = "bill.gates@hotmail.com",
                    City = "Seattle",
                    Hours = 100.00m,
                    IsDeleted = false
                },
                new Client
                {
                    ClientId = 4,
                    FirstName = "Barack",
                    LastName = "Obama",
                    Email = "former.pres@us.gov",
                    City = "Washington DC",
                    Hours = 15.00m,
                    IsDeleted = false
                }
            };
        }

        public static Sale[] SeedSales()
        {
            var sales = new List<Sale>();
            const int salesRepCount = 4;
            const int clientCount = 5;
            decimal[] percentages = new decimal[] { 0.35m, 0.65m, 1.0m };
            var random = new Random();

            for (int i = 1; i < 150; i++)
            {
                Guid correlationId = Guid.NewGuid();
                int salesRepId = random.Next(1, salesRepCount);
                int clientId = random.Next(1, clientCount);
                decimal percentage = percentages[random.Next(0, percentages.Length)];
                decimal hours = random.Next(1, 100) * 1.0m;
                DateTime soldDate = DateTime.Now.AddDays(random.Next(-90, 0));
                if (percentage == 1.0m)
                {
                    sales.Add(new Sale
                    {
                        SaleId = i,
                        CorrelationId = correlationId,
                        ClientId = clientId,
                        SalesRepId = salesRepId,
                        Hours = Math.Round(hours, 2),
                        PercentageOfSale = Math.Round(percentage * 100.0m, 2),
                        SoldDate = soldDate
                    });
                }
                else
                {
                    sales.Add(new Sale
                    {
                        SaleId = i,
                        CorrelationId = correlationId,
                        ClientId = clientId,
                        SalesRepId = salesRepId,
                        Hours = Math.Round(hours * percentage, 2),
                        PercentageOfSale = Math.Round(percentage * 100.0m, 2),
                        SoldDate = soldDate
                    });

                    decimal remainingPercentage = 1.0m - percentage;
                    int nextSalesRepId = random.Next(1, salesRepCount);

                    sales.Add(new Sale
                    {
                        SaleId = ++i,
                        CorrelationId = correlationId,
                        ClientId = clientId,
                        SalesRepId = nextSalesRepId,
                        Hours = Math.Round(hours * remainingPercentage, 2),
                        PercentageOfSale = Math.Round(remainingPercentage * 100.0m, 2),
                        SoldDate = soldDate
                    });
                }
            }

            return sales.ToArray();
        }
    }
}