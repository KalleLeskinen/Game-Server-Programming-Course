using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1.RealTimeBikes
{
    class OfflineStation
    {

        //name of the station
        public string name { get; set; }
        //Bikes at the station
        public int bikes { get; set; }


        public OfflineStation(string name, int bikes)
        {
            this.name = name;
            this.bikes = bikes;
        }


    }
}
