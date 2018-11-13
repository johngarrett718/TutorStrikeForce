using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TutorStrikeForce.EF;
using TutorStrikeForce.Models;
using TutorStrikeForce.ViewModels;

namespace TutorStrikeForce.Controllers
{
    public class SaleController : Controller
    {
        private readonly TutorStrikeForceContext _context;
        private IMapper _mapper;

        public List<SalesRep> SalesReps { get; set; }

        public SaleController(TutorStrikeForceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult Index(int month)
        {
            var monthYearOptions = new List<MonthYearOption>();
            if (month <= 0 || month > 12)
            {
                month = DateTime.Today.Month;
            }

            for (int i = 1; i < 13; i++)
            {
                monthYearOptions.Add(new MonthYearOption
                {
                    Month = i,
                    Year = DateTime.Today.Year
                });
            }

            var salesReps = _context.SalesReps.Include(s => s.Sales).Select(sr => new SalesRep
            {
                SalesRepId = sr.SalesRepId,
                FirstName = sr.FirstName,
                LastName = sr.LastName,
                Sales = sr.Sales.Where(s => s.SoldDate.Month == month)
            });

            var model = new SalesSummaryViewModel(monthYearOptions)
            {
                Month = month,
                Year = DateTime.Today.Year,
                SalesReps = salesReps.ToList()
            };

            return View(model);
        }

        public IActionResult ClientSales(int salesRepId, int month, int day)
        {
            var salesByClientViewModels = (
                from s in _context.Sales
                join c in _context.Clients on s.ClientId equals c.ClientId
                where s.SalesRepId == salesRepId && s.SoldDate.Month == month && s.SoldDate.Date.Day == day
                select new SalesByClientViewModel
                {
                    ClientId = c.ClientId,
                    Hours = s.Hours,
                    PercentageOfSale = s.PercentageOfSale,
                    ClientCity = c.City,
                    ClientName = $"{c.FirstName} {c.LastName}"
                }
            );

            var salesListViewModel = new SalesByClientSummaryViewModel
            {
                SalesByClient = salesByClientViewModels.ToList()
            };

            return PartialView(salesListViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var clientSelectItems = new List<SelectListItem>();
            var salesRepSelectItems = new List<SelectListItem>();

            var clients = _context.Clients.Where(client => !client.IsDeleted);
            var salesReps = _context.SalesReps.Where(salesRep => !salesRep.IsDeleted);

            clients.ToList().ForEach(client =>
            {
                clientSelectItems.Add(new SelectListItem
                {
                    Text = $"{client.FirstName} {client.LastName}",
                    Value = client.ClientId.ToString()
                });
            });

            salesReps.ToList().ForEach(rep =>
            {
                salesRepSelectItems.Add(new SelectListItem
                {
                    Text = rep.FullName,
                    Value = rep.SalesRepId.ToString()
                });
            });

            return View(new SaleEditModel
            {
                Clients = clientSelectItems,
                SalesReps = salesRepSelectItems
            });
        }

        [HttpPost]
        public IActionResult Create(SaleEditModel saleEditModel)
        {
            if (ModelState.IsValid)
            {
                int clientId = CreateOrUpdateClient(saleEditModel.Client, saleEditModel.Hours);
                Guid correlationId = Guid.NewGuid();

                if (saleEditModel.OpenerSalesRepId == saleEditModel.CloserSalesRepId)
                {
                    var sale = _mapper.Map<Sale>(saleEditModel);
                    sale.CorrelationId = correlationId;
                    sale.ClientId = clientId;
                    sale.PercentageOfSale = 100.00m;
                    sale.SalesRepId = saleEditModel.OpenerSalesRepId;
                    _context.Sales.Add(sale);
                }
                else
                {
                    var openerSale = _mapper.Map<Sale>(saleEditModel);
                    openerSale.ClientId = clientId;
                    openerSale.CorrelationId = correlationId;
                    openerSale.PercentageOfSale = 65.00m;
                    openerSale.Hours = saleEditModel.Hours * 0.65m;
                    openerSale.SalesRepId = saleEditModel.OpenerSalesRepId;
                    _context.Sales.Add(openerSale);

                    var closerSale = _mapper.Map<Sale>(saleEditModel);
                    closerSale.CorrelationId = correlationId;
                    closerSale.ClientId = clientId;
                    closerSale.PercentageOfSale = 35.00m;
                    closerSale.Hours = saleEditModel.Hours * 0.35m;
                    closerSale.SalesRepId = saleEditModel.CloserSalesRepId;
                    _context.Sales.Add(closerSale);
                }

                _context.SaveChanges();

                return RedirectToAction("Index", "Sale");
            }
            else
            {
                return View(saleEditModel);
            }
        }

        private int CreateOrUpdateClient(ClientEditModel clientEditModel, decimal hours)
        {
            int id = 0;
            if (clientEditModel.ClientId <= 0 || clientEditModel.ClientId == null)
            {
                var newClient = _mapper.Map<Client>(clientEditModel);
                newClient.Hours = hours;
                _context.Clients.Add(newClient);
                _context.SaveChanges();
                id = newClient.ClientId;
            }
            else
            {
                var client = _context.Clients.Find(clientEditModel.ClientId);
                client.Hours += hours;
                _context.Clients.Update(client);
                _context.SaveChanges();
                id = client.ClientId;
            }

            return id;
        }
    }
}