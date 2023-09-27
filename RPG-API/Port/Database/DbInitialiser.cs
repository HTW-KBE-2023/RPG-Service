using API.Models.Fights;
using API.Models.Monsters;
using API.Models.Players;

namespace API.Port.Database
{
    public class DbInitialiser
    {
        private readonly RPGContext _context;

        public DbInitialiser(RPGContext context)
        {
            _context = context;
        }

        public void Run()
        {
            _context.Database.EnsureCreated();
        }
    }
}