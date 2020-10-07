using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekti
{
    public class RaceTrack
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        //The fastest time you can do on the track
        public float FastestPossible { get; set; }

        public DateTime CreationTime { get; set; }


    }
}
