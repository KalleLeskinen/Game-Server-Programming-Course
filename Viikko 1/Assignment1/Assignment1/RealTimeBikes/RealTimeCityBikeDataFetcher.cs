using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Data;

namespace Assignment1.RealTimeBikes
{
    class RealTimeCityBikeDataFetcher : Interfaces.ICityBikeDataFetcher
    {

        BikeRentalStationList bl;

        static readonly HttpClient hClient = new HttpClient();


        //Returns the amount of bikes at the station
        public async Task<int> GetBikeCountInStation(string stationName)
        {

            bl = (BikeRentalStationList)await GetJson();

            try
            {

                foreach (Station s in bl.stations)
                {
                    if (s.name.Equals(stationName))
                    {
                        //Console.WriteLine(s.ToString());

                        return s.bikesAvailable;

                    }
                }

                throw new NotFoundException(stationName + " not found!");

            } catch (NotFoundException na)
            {
                Console.WriteLine("Error: " + na.Message + "\n\nPress ANY key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }

            return 0;

        }


        private async Task<object> GetJson()
        {
            try
            {
                HttpResponseMessage response = await hClient.GetAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return readJson(responseBody);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error!\n" + e.Message);
                return 0;
            }
        }


        //Reads the json to a bikerentalstationlist
        private BikeRentalStationList readJson(string responseBody)
        {

            var readStations = JsonConvert.DeserializeObject<BikeRentalStationList>(responseBody);
            return readStations;

        }



        //private void test(string responseBody)
        //{

        //    DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(responseBody);
        //    DataTable dataTable = dataSet.Tables["stations"];

        //    Console.WriteLine(dataTable.Rows.Count + " rows contained in the read json");


        //    foreach(DataRow r in dataTable.Rows)
        //    {
        //        Console.WriteLine(r["id"] + " - " + r["name"]);
        //    }

        //} 













    }
}
