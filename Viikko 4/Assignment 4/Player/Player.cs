using System;
using System.Collections.Generic;


namespace Assignment_4
{
    public class Player
    {

        public Player()
        {
            Items = new List<Item>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public List<Item> Items { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
