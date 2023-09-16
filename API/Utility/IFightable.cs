namespace API.Utility
{
    public interface IFightable
    {
        string Name { get; }

        int Level { get; }
        int Health { get; }
        int Defence { get; }
        int Attack { get; }
        double Experience { get; }

        void TakesDamage(int damage);
    }
}