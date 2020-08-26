using System;
using System.Threading.Tasks;

namespace Assignment1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(args[0]);

            RealTimeCityBikeDataFetcher cityBikes = new RealTimeCityBikeDataFetcher();

            Console.WriteLine(cityBikes.GetBikeCountInStation(args[0]));


        }

        
    }

    public interface ICityBikeDataFetcher
    {
        Task<int> GetBikeCountInStation(string stationName);
    }

}
