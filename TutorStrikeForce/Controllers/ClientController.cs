using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TutorStrikeForce.EF;
using TutorStrikeForce.Models;
using TutorStrikeForce.ViewModels;

namespace TutorStrikeForce.Controllers
{
    public class ClientController : Controller
    {
        private readonly TutorStrikeForceContext _context;
        private readonly IMapper _mapper;

        public ClientController(TutorStrikeForceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetByEmail(string email)
        {
            var client = _context.Clients.SingleOrDefault(c => c.Email == email);

            return new JsonResult(client);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClientEditModel clientModel)
        {
            if (ModelState.IsValid)
            {
                var id = _context.Clients.Add(_mapper.Map<Client>(clientModel));

                _context.SaveChanges();

                return RedirectToAction("ClientList", "Client");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ClientEditModel clientModel, int clientId)
        {
            if (ModelState.IsValid)
            {
                var id = _context.Clients.Update(_mapper.Map<Client>(clientModel));

                _context.SaveChanges();

                return RedirectToAction("ClientList", "Client");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Update(int clientId)
        {
            Client client = _context.Clients.Single(c => c.ClientId == clientId);
            if (client != null)
            {
                return View(_mapper.Map<ClientEditModel>(client));              
            }
            else
            {
                return RedirectToAction("ClientList", "Client");
            }
        }

        [HttpGet]
        public IActionResult Delete(int clientId)
        {
            var client = _context.Clients.Find(clientId);
            _context.Clients.Remove(client);
            _context.SaveChanges();
            return RedirectToAction("ClientList", "Client");
        }

        public IActionResult ClientList()
        {
            var clients = _context.Clients;

            var viewModel = new ClientViewModel
            {
                Clients = clients.ToList()
            };

            return View(viewModel);
        }
    }
}
