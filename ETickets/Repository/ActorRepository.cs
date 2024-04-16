using ETickets.Data;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repository
{
    public class ActorRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public Actor ReadActorById(int id)
        {

            return context.Actors.Find(id);

        }
    }
}
