namespace API.Services.Dice
{
    public class DiceService : IDiceService
    {
        private readonly int _minValue = 1;
        private readonly int _diceValue;
        private readonly Random _random;

        public DiceService(Random random, DiceValue diceValue)
        {
            _random = random;
            _diceValue = (int)diceValue;
        }

        public bool RollAgainst(int threshold) => Roll() >= threshold;

        public int Roll() => _random.Next(_minValue, _diceValue);

        public int MaxValue => _diceValue;
        public int MinValue => _minValue;
    }
}