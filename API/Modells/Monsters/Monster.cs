using API.Utility;

namespace API.Modells.Monsters
{
    public class Monster : IFightable
    {
        private readonly double _experience;
        private int _health;

        public Monster(int level = 1)
        {
            Level = level;

            _health = MaxHealth;
            _experience = MaxHealth * 0.1 + Defence * 0.1 + Attack * 0.1;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public int Level { get; init; }
        public string Name { get; set; } = "No-Name-Monster";
        public int BaseHealth { get; set; } = 1;
        public int BaseDefence { get; set; } = 1;
        public int BaseAttack { get; set; } = 1;
        public int LevelMultiplierHealth { get; set; } = 1;
        public int LevelMultiplierDefence { get; set; } = 1;
        public int LevelMultiplierAttack { get; set; } = 1;
        public int MaxHealth => BaseHealth + Level * LevelMultiplierHealth;

        public int Health => _health;

        public int Defence => BaseDefence + Level * LevelMultiplierDefence;
        public int Attack => BaseAttack + Level * LevelMultiplierAttack;
        public double Experience => _experience;

        public void TakesDamage(int damage)
        {
            _health -= damage;
        }
    }
}