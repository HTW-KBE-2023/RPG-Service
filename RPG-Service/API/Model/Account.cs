namespace API.Model
{
    public class Account
    {
        public Guid Id { get; set; }

        public int Experience { get; set; }
        public int Level { get; set; }

        public ICollection<Equipment> Equipment { get; set; }

        public int Health { get; }
        public int Defence { get; }
        public int Attack { get; }
    }
}