using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2.World
{
    public class Item
    {

        public Guid Id { get; set; }
        public int Level { get; set; }

        public Item(int level)
        {
            this.Id = Guid.NewGuid();
            this.Level = level;
        }

    }
}
