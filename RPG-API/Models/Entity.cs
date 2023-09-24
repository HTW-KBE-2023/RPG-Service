namespace API.Models
{
    public abstract class Entity
    {
        public Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}