using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekti
{
    public class Racer
    {

        public Racer()
        {
            LapTimes = new List<LapTime>();
        }


        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<LapTime> LapTimes { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreationTime { get; set; }



    }
}
