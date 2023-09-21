using API.Models.Fights;
using API.Models.Monsters;
using API.Models.Players;
using API.Services.Dice;
using Boxed.Mapping;
using MassTransit;
using MessagingContracts.RPG;

namespace API.Services.Fights
{
    public class FightSimulator
    {
        private readonly IDiceService _diceService;
        private readonly IGenericService<Fight> _fightService;
        private readonly IGenericService<Player> _playerService;
        private readonly IGenericService<Monster> _monsterService;
        private readonly IMapper<Fight, FightConcluded> _fightToFightConcludedMapper;
        private readonly IBus _bus;

        public FightSimulator(IDiceService diceService,
                              IGenericService<Fight> fightService,
                              IGenericService<Player> playerService,
                              IGenericService<Monster> monsterService,
                              FightMapper fightmapper,
                              IBus bus)
        {
            _diceService = diceService;
            _fightService = fightService;
            _playerService = playerService;
            _monsterService = monsterService;
            _fightToFightConcludedMapper = fightmapper;
            _bus = bus;
        }

        public void SimulationFor(FightRequested fightRequest)
        {
            Fight fight = PrepareForSimulation(fightRequest.Player);

            _fightService.Create(fight);

            var simulation = new FightSimulation(_diceService, fight);
            simulation.OnComplete += NotifyServices;
        }

        private void NotifyServices(object? sender, Fight fight)
        {
            _fightService.Update(fight);

            var concludedMessage = _fightToFightConcludedMapper.Map(fight);
            _bus.Publish(concludedMessage);
        }

        private Fight PrepareForSimulation(Guid playerId)
        {
            return new()
            {
                Enemy = GetRandomMonster(),
                Player = GetOrCreatePlayer(playerId)
            };
        }

        private Monster GetRandomMonster()
        {
            var monsters = _monsterService.GetAll();
            var monster = monsters.ElementAt(Random.Shared.Next(0, monsters.Count()));

            return monster;
        }

        private Player GetOrCreatePlayer(Guid playerId)
        {
            var player = _playerService.GetById(playerId);

            if (player is null)
            {
                var result = _playerService.Create(new Player()
                {
                    Name = "Default Name",
                    Level = 1,
                    Experience = 0,
                    Attack = 5,
                    Defence = 5,
                    Health = 15
                });

                player = result.Value ?? throw new ArgumentException($"Player for Id {playerId} could not be found or createt inside of the FightSimulator.", nameof(playerId));
            }
            return player;
        }
    }
}