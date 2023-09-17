using API.Models.Monsters;
using API.Models.Players;
using API.Services.Dice;
using API.Utility;

namespace API.Models.Fights
{
    public class Fight
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Player Player { get; set; } = new Player();
        public Monster Enemy { get; set; } = new Monster();
        public IList<string> Summary { get; set; } = new List<string>();
        public bool Completed { get; set; }

        public void Simulate(IDiceService dice)
        {
            WriteSummary($"{Player.Name} is encountering {Enemy.Name}");
            IFightable[] _initiative = DecideInitiative(dice);
            WriteSummary($"{_initiative.First().Name} has the first action!");

            ExecuteFightingLoop(dice, _initiative);
            WriteVictoryLog();

            Completed = true;
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
            } while (Enemy.Health > 1 && Player.Health > 1);
        }

        private void WriteVictoryLog()
        {
            if (Player.Health > 0)
            {
                WriteSummary($"{Player.Name} won the fight and gets {Enemy.Experience} Experience as a reward!");
            }
            else
            {
                WriteSummary($"{Player.Name} lost the fight against {Enemy.Name}!");
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

        private void WriteSummary(string text) => Summary.Add(text);

        private IFightable[] DecideInitiative(IDiceService dice)
        {
            return new List<(int initative, IFightable character)>
            {
                { (dice.Roll() + Player.Level, Player) },
                { (dice.Roll() + Enemy.Level, Enemy) }
            }
            .OrderBy(selection => selection.initative)
            .Select(selection => selection.character)
            .ToArray();
        }
    }
}