using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Assignment1.RealTimeBikes;


namespace Assignment1
{
    class Program
    {



        static async Task Main(string[] args)
        {


            bool connected = false;

            try
            {
                ValidateInput(args[0]);
            } catch
            {
                Console.WriteLine("No arguments found.\n\nPress ANY key to continue...");
                Console.ReadKey();
                Environment.Exit(0);
            }




            if (args[1].Equals("online"))
            {
                connected = true;
            }
            else
            {
                connected = false;
            }


            if (connected)
            {
                Console.WriteLine("Searching Online for " + args[0] + "\n");

                RealTimeCityBikeDataFetcher cityBikes = new RealTimeCityBikeDataFetcher();

                Console.WriteLine("Bikes available at " + args[0] + ": " + await cityBikes.GetBikeCountInStation(args[0]));
            }
            else
            {
                Console.WriteLine("Searching Offline for " + args[0] + "\n");

                OfflineCityBikeDataFetcher cityBikes = new OfflineCityBikeDataFetcher();

                Console.WriteLine("Bikes available at " + args[0] + ": " + await cityBikes.GetBikeCountInStation(args[0]));
            }


        }


        static void ValidateInput(string input)
        {

            if (input.Any(char.IsDigit))
            {
                throw new ArgumentException(String.Format("Input: {0} contains numbers!", input), "input");
            }
        }

    }

}
