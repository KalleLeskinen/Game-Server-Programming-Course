using Newtonsoft.Json;
using System.Collections.Generic;

namespace Assignment1.RealTimeBikes
{
    public class Station
    {

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("x")]
        public float x { get; set; }
        [JsonProperty("y")]
        public float y { get; set; }
        [JsonProperty("bikesAvailable")]
        public int bikesAvailable { get; set; }
        [JsonProperty("spacesAvailable")]
        public int spacesAvailable { get; set; }
        [JsonProperty("allowDropoff")]
        public bool allowDropoff { get; set; }
        [JsonProperty("isFloatingBike")]
        public bool isFloatingBike { get; set; }
        [JsonProperty("isCarStation")]
        public bool isCarStation { get; set; }
        [JsonProperty("state")]
        public string state { get; set; }
        [JsonProperty("networks")]
        public List<string> networks { get; set; }
        [JsonProperty("realTimeData")]
        public bool realTimeData { get; set; }
    
        override
        public string ToString()
        {
            return id + " - " + name + " - " + x + " - " + y + " - " + bikesAvailable + " - " + allowDropoff;
        } 
    
    
    }



}