using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekti
{
    public class LapTime
    {

        public Guid Id { get; set; }

        public float Time { get; set; }

        public RaceTrack Track { get; set; }

        public DateTime CreationTime { get; set; }



    }
}
