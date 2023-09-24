namespace API.Services.Dice
{
    public interface IDiceService
    {
        bool RollAgainst(int threshold);

        int Roll();

        int MaxValue { get; }
        int MinValue { get; }
    }
}