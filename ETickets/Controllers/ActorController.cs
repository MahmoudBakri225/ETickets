using ETickets.Data;
using ETickets.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Controllers
{
    public class ActorController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {

            var Actors = context.Actors.ToList();

            return View(Actors);

        }
        public ActionResult Details(int id)
        {
            Actor actor = context.Actors.Find(id);
            
            return View(actor);
        }

         
    }
}

