using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TutorStrikeForce.Models;

namespace TutorStrikeForce.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        //[Route("[action]")]
        public IActionResult Index()
        {
            var salesReps = new List<SalesRep>
            {
                new SalesRep
                {
                    FirstName = "John",
                    LastName = "Garrett"
                }
            };
            return Content("Good evening");
        }
    }
}
