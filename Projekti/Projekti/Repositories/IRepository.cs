using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekti
{
    public interface IRepository
    {
        Task<Racer> Get(Guid id);
        Task<Racer[]> GetAll();
        Task<Racer> Create(Racer player);
        Task<Racer> Delete(Guid id);

        Task<LapTime> SetLapTime(Guid playerId, LapTime item);
        Task<LapTime> GetLapTime(Guid playerId, Guid itemId);
        Task<LapTime[]> GetAllLapTimes(Guid playerId);
        Task<LapTime> DeleteLapTime(Guid playerId, Guid lapId);


        Task<RaceTrack> CreateTrack(RaceTrack raceTrack);
        Task<RaceTrack> GetTrack(Guid id);
        Task<RaceTrack[]> GetAllTracks();
        Task<RaceTrack> UpdateTrack(Guid id, RaceTrack RaceTrack);
        Task<RaceTrack> DeleteTrack(Guid id);


    }
}
