﻿using API.Models.Monsters;
using API.Models.Players;

namespace API.Models.Fights
{
    public class Fight : Entity
    {
        public Fight() : base(Guid.NewGuid())
        {
        }

        public Player Player { get; set; } = new Player();
        public Monster Enemy { get; set; } = new Monster();
        public IList<string> Summary { get; set; } = new List<string>();
        public bool Completed { get; set; }
    }
}