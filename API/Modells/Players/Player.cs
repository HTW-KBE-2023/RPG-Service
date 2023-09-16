﻿using System.Xml.Linq;
using API.Modells.Fights;
using API.Utility;
using Microsoft.AspNetCore.Connections;

namespace API.Modells.Players
{
    public class Player : IFightable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "No-Name-Player";
        public int Level { get; set; } = 1;
        public int Health { get; set; } = 5;
        public int Defence { get; set; } = 1;
        public int Attack { get; set; } = 1;
        public double Experience { get; set; } = 0;

        public IList<Fight> Fights { get; set; } = new List<Fight>();

        public void TakesDamage(int damage)
        {
            Health -= damage;
        }
    }
}