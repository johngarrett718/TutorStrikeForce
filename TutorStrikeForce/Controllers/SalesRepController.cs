using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TutorStrikeForce.EF;
using TutorStrikeForce.Models;
using TutorStrikeForce.ViewModels;

namespace TutorStrikeForce.Controllers
{
    public class SalesRepController : Controller
    {
        private readonly TutorStrikeForceContext _context;
        private readonly IMapper _mapper;

        public SalesRepController(TutorStrikeForceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SalesRepEditModel salesRepEditModel)
        {
            if (ModelState.IsValid)
            {
                _context.SalesReps.Add(_mapper.Map<SalesRep>(salesRepEditModel));
                _context.SaveChanges();
                return RedirectToAction("SalesRepList", "SalesRep");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Update(int salesRepId)
        {
            SalesRep salesRep = _context.SalesReps.Single(s => s.SalesRepId == salesRepId);
            if(salesRep != null)
            {
                return View(_mapper.Map<SalesRepEditModel>(salesRep));
            }
            else
            {
                return RedirectToAction("SalesRepList", "SalesRep");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(SalesRepEditModel salesRepEditModel, int salesRepId)
        {
            if (ModelState.IsValid)
            {
                _context.SalesReps.Update(_mapper.Map<SalesRep>(salesRepEditModel));
                _context.SaveChanges();
                return RedirectToAction("SalesRepList", "SalesRep");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int salesRepId)
        {
            var salesRep = _context.SalesReps.Find(salesRepId);
            _context.SalesReps.Remove(salesRep);
            _context.SaveChanges();
            return RedirectToAction("SalesRepList", "SalesRep");
        }

        public IActionResult SalesRepList()
        {
            var salesreps = _context.SalesReps;
            var viewModel = new SalesRepViewModel
            {
                SalesReps = salesreps.ToList()
            };

            return View(viewModel);
        }
    }
}
