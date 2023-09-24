using System.Xml.Linq;
using API.Models.Fights;
using Microsoft.AspNetCore.Connections;

namespace API.Models.Players
{
    public class Player : IEntity, IFightable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "No-Name-Player";
        public int Level { get; set; } = 1;
        public int Health { get; set; } = 5;
        public int Defence { get; set; } = 1;
        public int Attack { get; set; } = 1;
        public double Experience { get; set; } = 0;

        public void TakesDamage(int damage)
        {
            Health -= damage;
        }
    }
}