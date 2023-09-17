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
            RecreateDatabase();
            AddTestData();
        }

        private void AddTestData()
        {
            var monster = new Monster()
            {
                Id = Guid.NewGuid(),
                Name = "Dummy",
                BaseAttack = 1,
                BaseDefence = 3,
                BaseHealth = 5,
                LevelMultiplierAttack = 2,
                LevelMultiplierDefence = 2,
                LevelMultiplierHealth = 2,
            };
            _context.Add(monster);

            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Name = "Max Mustermann",
                Attack = 3,
                Defence = 4,
                Health = 10,
                Experience = 0,
                Level = 2
            };
            _context.Add(player);

            _context.Add(new Fight()
            {
                Id = Guid.NewGuid(),
                Completed = true,
                Player = player,
                Enemy = monster,
                Summary = new List<string>()
                {
                    "Test Summary 1",
                    "Test Summary 2",
                    "Test Summary 3"
                }
            });

            _context.SaveChanges();
        }

        private void RecreateDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}