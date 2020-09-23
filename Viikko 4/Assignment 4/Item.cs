using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_4
{
    public class Item
    {
        public Guid Id                  { get; set; }
        public int Level                { get; set; }
        public ItemType Type            { get; set; }
        public DateTime CreationTime    { get; set; }
    }

}
