using API.Models.Fights;
using API.Models;
using API.Services.Dice;
using RabbitMQ.Client.Events;

namespace API.Services.Fights
{
    public class FightSimulation
    {
        public event EventHandler<Fight>? OnComplete;

        private readonly IDiceService _diceService;
        private readonly Fight _fight;

        public FightSimulation(IDiceService diceService, Fight fight)
        {
            _diceService = diceService;
            _fight = fight;
        }

        public void Simulate()
        {
            WriteSummary($"{_fight.Player.Name} is encountering {_fight.Enemy.Name}");
            IFightable[] _initiative = DecideInitiative(_diceService);
            WriteSummary($"{_initiative.First().Name} has the first action!");

            ExecuteFightingLoop(_diceService, _initiative);
            WriteVictoryLog();

            _fight.Completed = true;

            OnComplete?.Invoke(this, _fight);
        }

        private void ExecuteFightingLoop(IDiceService dice, IFightable[] _initiative)
        {
            do
            {
                var activeFighter = _initiative[0];
                var passiveFighter = _initiative[1];

                if (dice.RollAgainst(10))
                {
                    WriteSummary($"{activeFighter.Name}'s attack is succesfull!");

                    var attackValue = activeFighter.Attack - passiveFighter.Defence;
                    var normalizedDamage = attackValue < 1 ? 0 : attackValue;

                    passiveFighter.TakesDamage(normalizedDamage);

                    WriteDamageLog(passiveFighter, normalizedDamage);
                }
                else
                {
                    WriteSummary($"{activeFighter.Name}'s attack missed!");
                }

                Array.Reverse(_initiative);
            } while (_fight.Enemy.Health > 1 && _fight.Player.Health > 1);
        }

        private void WriteVictoryLog()
        {
            if (_fight.Player.Health > 0)
            {
                WriteSummary($"{_fight.Player.Name} won the fight and gets {_fight.Enemy.Experience} Experience as a reward!");
            }
            else
            {
                WriteSummary($"{_fight.Player.Name} lost the fight against {_fight.Enemy.Name}!");
            }
        }

        private void WriteDamageLog(IFightable passiveFighter, int damage)
        {
            if (passiveFighter.Health > 0)
            {
                WriteSummary($"{passiveFighter.Name}'s health takes a hit of {damage} damage, now with only {passiveFighter.Health} health left!");
            }
            else
            {
                WriteSummary($"{passiveFighter.Name} has no health left and was defeated!");
            }
        }

        private void WriteSummary(string text) => _fight.Summary.Add(text);

        private IFightable[] DecideInitiative(IDiceService dice)
        {
            return new List<(int initative, IFightable character)>
        {
                { (dice.Roll() + _fight.Player.Level, _fight.Player) },
                { (dice.Roll() + _fight.Enemy.Level , _fight.Enemy) }
            }
            .OrderBy(selection => selection.initative)
            .Select(selection => selection.character)
            .ToArray();
        }
    }
}