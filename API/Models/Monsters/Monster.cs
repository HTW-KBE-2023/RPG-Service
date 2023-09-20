namespace API.Models.Monsters
{
    public class Monster : Entity, IFightable
    {
        private int _health;

        public Monster() : base(Guid.NewGuid())
        {
            _health = MaxHealth;
        }

        public int Level { get; set; } = 1;
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
        public double Experience => MaxHealth * 0.1 + Defence * 0.1 + Attack * 0.1;

        public void TakesDamage(int damage)
        {
            _health -= damage;
        }
    }
}