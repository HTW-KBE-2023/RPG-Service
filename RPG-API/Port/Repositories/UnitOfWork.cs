using API.Models;
using API.Models.Fights;
using API.Models.Monsters;
using API.Models.Players;
using API.Port.Database;

namespace API.Port.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed = false;

        private readonly RPGContext _context;

        private readonly ICollection<object> _genericRepositories = new List<object>();

        private IGenericRepository<Monster> _monsterRepository;
        private IGenericRepository<Player> _playerRepository;
        private IGenericRepository<Fight> _fightRepository;

        public UnitOfWork(RPGContext context)
        {
            _context = context;

            _genericRepositories.Add(new GenericRepository<Monster>(_context));
            _genericRepositories.Add(new GenericRepository<Player>(_context));
            _genericRepositories.Add(new GenericRepository<Fight>(_context));
        }

        public IGenericRepository<Monster> MonsterRepository
        {
            get
            {
                _monsterRepository ??= GetGenericRepository<Monster>();
                return _monsterRepository;
            }
        }

        public IGenericRepository<Player> PlayerRepository
        {
            get
            {
                _playerRepository ??= GetGenericRepository<Player>();
                return _playerRepository;
            }
        }

        public IGenericRepository<Fight> FightRepository
        {
            get
            {
                _fightRepository ??= GetGenericRepository<Fight>();
                return _fightRepository;
            }
        }

        public IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : IEntity
        {
            var genericRepository = _genericRepositories.OfType<IGenericRepository<TEntity>>().SingleOrDefault();
            if (genericRepository is null)
            {
                throw new ArgumentException($"No Repository for the Type {nameof(TEntity)} could be found!");
            }

            return genericRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}