using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.RealTimeBikes
{
    class OfflineCityBikeDataFetcher : Interfaces.ICityBikeDataFetcher
    {

        List<OfflineStation> offlineStations;

        //Returns the amount of bikes at the station
        public async Task<int> GetBikeCountInStation(string stationName)
        {

            offlineStations = GetOfflineData();


            try
            {

                foreach (OfflineStation s in offlineStations)
                {
                    if (s.name.Equals(stationName))
                    {
                        //Console.WriteLine(s.ToString());

                        return s.bikes;

                    }
                }
                throw new NotFoundException("Station " + stationName + " not found!");
            }
            catch (NotFoundException na)
            {
                Console.WriteLine("Error: " + na.Message + "\n\nPress ANY key to exit..." );
                Console.ReadKey();
                Environment.Exit(0);
            }


            return 0;

        }

        private List<OfflineStation> GetOfflineData()
        {

            List<OfflineStation> foundStations = new List<OfflineStation>();

            string filename = "bikedata.txt";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", filename);

            StreamReader reader = File.OpenText(path);

            string line;
            while ((line = reader.ReadLine()) != null)
            {

                string[] columns = line.Split(':');

                //Pitää trimmata pois whitespace .Trim(), muuten hakiessa ei löydy
                foundStations.Add(new OfflineStation(columns[0].Trim(), Int32.Parse(columns[1])));

            }

            return foundStations;
        }




    }
}
