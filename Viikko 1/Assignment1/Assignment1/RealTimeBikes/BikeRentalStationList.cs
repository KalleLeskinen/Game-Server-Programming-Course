using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

namespace Assignment1.RealTimeBikes
{
    /// <summary>
    /// Bikestation info
    /// </summary>

    public class BikeRentalStationList
    {
        [JsonProperty("stations")]
        public List<Station> stations{ get; set; }
    }


    
}
