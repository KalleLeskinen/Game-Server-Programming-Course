using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Projekti
{

    [ApiController]
    [Route("tracks")]

    public class TrackController : ControllerBase
    {

        private readonly ILogger<TrackController> _logger;
        private readonly IRepository _repository;

        public TrackController(ILogger<TrackController> logger, IRepository repo)
        {
            _logger = logger;
            _repository = repo;
        }

        [HttpPost]
        [Route("CreateTrack")]
        public async Task<RaceTrack> Create([FromBody] NewRaceTrack raceTrack)
        {

            RaceTrack newRaceTrack = new RaceTrack
            {
                Id = Guid.NewGuid(),
                Name = raceTrack.Name,
                FastestPossible = raceTrack.FastestPossible,
                CreationTime = DateTime.Now
            };

            return await _repository.CreateTrack(newRaceTrack);

        }

        [HttpPost]
        [Route("DeleteTrack/{id:Guid}")]
        public async Task<RaceTrack> DeleteTrack(Guid id)
        {
            return await _repository.DeleteTrack(id);
        }

        [HttpPut]
        [Route("UpdateTrack/{id:Guid}")]
        public async Task<RaceTrack> UpdateTrack(Guid id,[FromBody] NewRaceTrack raceTrack)
        {

            RaceTrack updatedTrack = new RaceTrack
            {
                Id = Guid.NewGuid(),
                Name = raceTrack.Name,
                FastestPossible = raceTrack.FastestPossible,
                CreationTime = DateTime.Now
            };

            return await _repository.UpdateTrack(id, updatedTrack);
        }

        [HttpGet]
        [Route("GetAllTracks")]
        public async Task<RaceTrack[]> GetAllTracks()
        {
            return await _repository.GetAllTracks();
        }



    }
}
