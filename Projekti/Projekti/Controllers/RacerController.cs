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
    [Route("racers")]

    public class RacerController : ControllerBase
    {

        private readonly ILogger<RacerController> _logger;
        private readonly IRepository _repository;


        public RacerController(ILogger<RacerController> logger, IRepository repo)
        {
            _logger = logger;
            _repository = repo;
        }


        [HttpPost]
        [Route("CreateRacer")]
        public async Task<Racer> Create([FromBody] NewRacer racer)
        {

            Racer newRacer = new Racer
            {
                Id = Guid.NewGuid(),
                Name = racer.Name,
                CreationTime = DateTime.Now
            };

            return await _repository.Create(newRacer);
        }

        [HttpPost]
        [Route("Delete/{id:Guid}")]
        public async Task<Racer> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }



        [HttpGet]
        [Route("GetAll")]
        public async Task<Racer[]> GetAll()
        {
            return await _repository.GetAll();
        }


        [HttpPost]
        [Route("{id:Guid}/SetNewLapTime/Track/{trackId:Guid}")]
        public async Task<LapTime> SetLapTime(Guid id, Guid trackId, [FromBody]NewLapTime lap)
        {

            LapTime newLapTime = new LapTime
            {
                
                Id = Guid.NewGuid(),
                Time = lap.Time,
                Track = await _repository.GetTrack(trackId),
                CreationTime = DateTime.Now

            };

            return await _repository.SetLapTime(id, newLapTime);
        }

        [HttpGet]
        [Route("{id:Guid}/GetAllLapTimes")]
        public async Task<LapTime[]> GetAllLapTimes(Guid id)
        {
            return await _repository.GetAllLapTimes(id);
        }


        [HttpPost]
        [Route("{id:Guid}/DeleteLapTime/LapId/{lapId:Guid}")]
        public async Task<LapTime> DeleteLapTime(Guid id, Guid lapId)
        {
            return await _repository.DeleteLapTime(id, lapId);
        }

    }


}
