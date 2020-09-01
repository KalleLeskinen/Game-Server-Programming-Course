using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.RealTimeBikes.Interfaces
{
    public interface ICityBikeDataFetcher
    {
        Task<int> GetBikeCountInStation(string stationName);
    }
}
